using Assets.Scripts.Quadrants;
using Assets.Scripts.Spaceship.Targeting;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Flocking {
    [UpdateAfter(typeof(QuadrantSystem))]
    public class FlockingSystem : SystemBase {
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;
            var quadrantHashMap = QuadrantSystem.QuadrantHashMap;

            var allObstacles = GetEntityQuery(ComponentType.ReadOnly<BoidObstacle>()).ToEntityArray(Allocator.TempJob);

            Entities.WithReadOnly(quadrantHashMap).WithAll<SpaceshipTag>().ForEach(
                (Entity entity, ref MovementComponent movement, in Boid boid, in Translation pos, in TargetingComponent target) => {
                    var neighborCnt = 0;
                    var cohesionPos = float3.zero; // For average position in neighborhood
                    var alignmentVec = float3.zero; // For average alignment vector
                    var separationVec = float3.zero; // For average separation vector

                    var allTranslations = GetComponentDataFromEntity<Translation>(true);

                    var hashMapKey = QuadrantSystem.HashKeyFromPosition(pos.Value);
                    SearchQuadrantNeighbors(in quadrantHashMap, hashMapKey, entity, boid.CellRadius, pos.Value,
                        ref neighborCnt, ref cohesionPos, ref alignmentVec, ref separationVec);

                    if (neighborCnt > 0) {
                        separationVec /= neighborCnt;
                        cohesionPos /= neighborCnt;
                        alignmentVec /= neighborCnt;
                    } else {
                        cohesionPos =
                            pos.Value; // Set the position to current position in order to create a zero vector as heading
                        alignmentVec = movement.Heading;
                        separationVec = movement.Heading;
                    }

                    var avoidanceHeading = float3.zero;
                    var avoidObstacle = false;
                    for (var i = 0; i < allObstacles.Length; i++) {
                        var otherPos = allTranslations[allObstacles[i]];

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


                    var pursuitSteering = float3.zero; 
                    if (target.TargetLocked) {
                        pursuitSteering = math.normalizesafe(target.TargetPosition - pos.Value) * boid.PursuitWeight;
                    }

                    var targetSteering = math.normalizesafe(movement.Target - pos.Value) * boid.TargetWeight;
                    var alignmentSteering = math.normalizesafe(alignmentVec) * boid.AlignmentWeight;
                    var cohesionSteering = math.normalizesafe(cohesionPos - pos.Value) * boid.CohesionWeight;
                    var separationSteering = math.normalizesafe(separationVec) * boid.SeparationWeight;
                    var normalHeading =
                        math.normalizesafe(targetSteering + alignmentSteering + cohesionSteering + separationSteering + pursuitSteering);

                    var targetHeading = avoidObstacle ? avoidanceHeading : normalHeading;
                    movement.Heading = math.normalizesafe(movement.Heading +
                                                          deltaTime * boid.SteeringSpeed *
                                                          (targetHeading - movement.Heading));
                }).Schedule();

            allObstacles.Dispose(Dependency);
        }

        private static void SearchQuadrantNeighbor(in NativeMultiHashMap<int, QuadrantData> quadrantHashMap, in int key,
            in Entity currentEntity, in float radius, in float3 pos, ref int neighborCnt, ref float3 cohesionPos,
            ref float3 alignmentVec, ref float3 separationVec) {
            if (!quadrantHashMap.TryGetFirstValue(key, out var quadrantData, out var iterator)) return;

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
