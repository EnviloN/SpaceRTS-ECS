using Assets.Scripts.Quadrants;
using Assets.Scripts.Spaceship.Movement;
using Assets.Scripts.Spaceship.Targeting;
using Assets.Scripts.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Flocking {
    /// <summary>
    /// System handles the flocking simulation of starships.
    /// </summary>
    /// <remarks>
    /// This system has to be updated after <c>QuadrantSystem</c>, because it neers the
    /// quadrant system to be built.
    /// </remarks>
    [UpdateAfter(typeof(QuadrantSystem))]
    public class FlockingSystem : SystemBase {
        /// <summary>
        /// Handles flocking simulation.
        /// </summary>
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;

            // Note that we need to pass the quadrant system structure to WithReadOnly(). This is because other systems are
            // reading from it in parallel and we need to ensure the safety system that we will not modify the memory.
            var quadrantHashMap = QuadrantSystem.QuadrantHashMap;

            // Query for all entities with BoidObstacle tag component
            var allObstacles = GetEntityQuery(ComponentType.ReadOnly<BoidObstacle>()).ToEntityArray(Allocator.TempJob);

            Entities.WithReadOnly(quadrantHashMap).WithReadOnly(allObstacles).WithAll<SpaceshipTag>().ForEach(
                (Entity entity, ref MovementComponent movement, in BoidComponent boid, in Translation pos,
                    in TargetingComponent target) => {
                    var neighborCnt = 0;
                    var cohesionPos = float3.zero; // For average position in neighborhood
                    var alignmentVec = float3.zero; // For average alignment vector
                    var separationVec = float3.zero; // For average separation vector

                    // Get all translation components (will be needed for the obstacle positions)
                    var allTranslations = GetComponentDataFromEntity<Translation>(true);

                    // Search neighboring quadrants and update aggregate steering data
                    var hashMapKey = QuadrantSystem.HashKeyFromPosition(pos.Value);
                    SearchQuadrantNeighbors(in quadrantHashMap, hashMapKey, entity, boid.CellRadius, pos.Value,
                        ref neighborCnt, ref cohesionPos, ref alignmentVec, ref separationVec);

                    // Average steering data
                    if (neighborCnt > 0) {
                        separationVec /= neighborCnt;
                        cohesionPos /= neighborCnt;
                        alignmentVec /= neighborCnt;
                    } else {
                        // apply no steering
                        cohesionPos = pos.Value;
                        alignmentVec = movement.Heading;
                        separationVec = movement.Heading;
                    }

                    // Compute obstacle avoidance
                    var avoidanceHeading = float3.zero;
                    var avoidObstacle = false; // if the boid should avoid obstacle instead of normal steering
                    for (var i = 0; i < allObstacles.Length; i++) {
                        var otherPos = allTranslations[allObstacles[i]]; // Obstacle position

                        var distance = Vector3.Distance(pos.Value, otherPos.Value);
                        if (distance >= boid.ObstacleAversionDistance) continue; // This obstacle is not close

                        avoidObstacle = true;
                        var obstacleSteering = pos.Value - otherPos.Value;
                        if (obstacleSteering.Equals(float3.zero))
                            obstacleSteering = movement.Heading;
                        obstacleSteering = math.normalizesafe(obstacleSteering);

                        avoidanceHeading = otherPos.Value + obstacleSteering * boid.ObstacleAversionDistance -
                                           pos.Value;
                        break; // We have found an obstacle to avoid
                    }

                    // Check if we are in pursuit and compute pursuit steering
                    var pursuitSteering = float3.zero;
                    if (target.TargetLocked) {
                        pursuitSteering = math.normalizesafe(target.TargetPosition - pos.Value) * boid.PursuitWeight;
                    }

                    // Combine all steering factors and decide which one to use
                    var targetSteering = math.normalizesafe(movement.Target - pos.Value) * boid.TargetWeight;
                    var alignmentSteering = math.normalizesafe(alignmentVec) * boid.AlignmentWeight;
                    var cohesionSteering = math.normalizesafe(cohesionPos - pos.Value) * boid.CohesionWeight;
                    var separationSteering = math.normalizesafe(separationVec) * boid.SeparationWeight;
                    var normalHeading = math.normalizesafe(targetSteering + alignmentSteering + cohesionSteering +
                                                           separationSteering + pursuitSteering);

                    var targetHeading = avoidObstacle ? avoidanceHeading : normalHeading;

                    // Setting a new heading (shifted towards target heading)
                    movement.Heading = math.normalizesafe(movement.Heading +
                                                          deltaTime * boid.SteeringSpeed *
                                                          (targetHeading - movement.Heading));
                }).ScheduleParallel();

            // Dispose the NativeArray of obstacle entities
            allObstacles.Dispose(Dependency);
        }

        /// <summary>
        ///  Method performs a search and aggregation of steering elements inside a given quadrant.
        /// </summary>
        private static void SearchQuadrantNeighbor(in NativeMultiHashMap<int, QuadrantData> quadrantHashMap, in int key,
            in Entity currentEntity, in float radius, in float3 pos, ref int neighborCnt, ref float3 cohesionPos,
            ref float3 alignmentVec, ref float3 separationVec) {
            // check if there are any starships in the quadrant
            if (!quadrantHashMap.TryGetFirstValue(key, out var quadrantData, out var iterator)) return;

            // loop through all starships in the quadrant and aggregate steering elements if close enough
            do {
                if (currentEntity == quadrantData.Entity)
                    continue;

                var distance = Vector3.Distance(pos, quadrantData.Position);
                if (distance < radius) {
                    neighborCnt++;
                    separationVec +=
                        (pos - quadrantData.Position) /
                        distance; // vector from other boid to this boid inversely proportional to distance
                    cohesionPos += quadrantData.Position;
                    alignmentVec += quadrantData.Rotation;
                }
            } while (quadrantHashMap.TryGetNextValue(out quadrantData, ref iterator));
        }

        /// <summary>
        ///  Method performs a search and aggregation of steering elements inside all neighboring quadrants.
        /// </summary>
        private static void SearchQuadrantNeighbors(in NativeMultiHashMap<int, QuadrantData> quadrantHashMap,
            in int key, in Entity currentEntity, in float radius, in float3 pos, ref int neighborCnt,
            ref float3 cohesionPos, ref float3 alignmentVec, ref float3 separationVec) {
            SearchQuadrantNeighbor(quadrantHashMap, key, currentEntity, radius, pos, ref neighborCnt, ref cohesionPos,
                ref alignmentVec, ref separationVec);
            SearchQuadrantNeighbor(quadrantHashMap, key + 1, currentEntity, radius, pos, ref neighborCnt,
                ref cohesionPos, ref alignmentVec, ref separationVec);
            SearchQuadrantNeighbor(quadrantHashMap, key - 1, currentEntity, radius, pos, ref neighborCnt,
                ref cohesionPos, ref alignmentVec, ref separationVec);
            SearchQuadrantNeighbor(quadrantHashMap, key + QuadrantSystem.QuadrantYMultiplier, currentEntity, radius,
                pos, ref neighborCnt, ref cohesionPos, ref alignmentVec, ref separationVec);
            SearchQuadrantNeighbor(quadrantHashMap, key - QuadrantSystem.QuadrantYMultiplier, currentEntity, radius,
                pos, ref neighborCnt, ref cohesionPos, ref alignmentVec, ref separationVec);
        }
    }
}
