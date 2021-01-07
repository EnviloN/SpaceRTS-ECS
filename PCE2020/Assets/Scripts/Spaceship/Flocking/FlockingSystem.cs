using Assets.Scripts.Quadrants;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Spaceship.Flocking {
    public class FlockingSystem : SystemBase {
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;
            var quadrantHashMap = QuadrantSystem.QuadrantHashMap;

            var allObstacles = GetEntityQuery(ComponentType.ReadOnly<BoidObstacle>())
                .ToEntityArray(Allocator.TempJob);

            Entities.WithAll<SpaceshipTag>().ForEach(
                (Entity entity, ref MovementComponent movement, in Boid boid, in Translation pos) => {
                    var neighborCnt = 0;
                    var cohesionPos = float3.zero; // For average position in neighborhood
                    var alignmentVec = float3.zero; // For average alignment vector
                    var separationVec = float3.zero; // For average alignment vector

                    var allTranslations = GetComponentDataFromEntity<Translation>(true);

                    var hashMapKey = QuadrantSystem.HashKeyFromPosition(pos.Value);
                    SearchQuadrantNeighbors(quadrantHashMap, hashMapKey, entity, boid, pos, ref neighborCnt,
                        ref cohesionPos, ref alignmentVec, ref separationVec);
                    SearchQuadrantNeighbors(quadrantHashMap, hashMapKey + 1, entity, boid, pos, ref neighborCnt,
                        ref cohesionPos, ref alignmentVec, ref separationVec);
                    SearchQuadrantNeighbors(quadrantHashMap, hashMapKey - 1, entity, boid, pos, ref neighborCnt,
                        ref cohesionPos, ref alignmentVec, ref separationVec);
                    SearchQuadrantNeighbors(quadrantHashMap, hashMapKey + QuadrantSystem.QuadrantYMultiplier, entity,
                        boid, pos, ref neighborCnt, ref cohesionPos, ref alignmentVec, ref separationVec);
                    SearchQuadrantNeighbors(quadrantHashMap, hashMapKey - QuadrantSystem.QuadrantYMultiplier, entity,
                        boid, pos, ref neighborCnt, ref cohesionPos, ref alignmentVec, ref separationVec);

                    var avoidanceHeading = float3.zero;
                    var avoidObstacle = false;
                    for (var i = 0; i < allObstacles.Length; i++) {
                        Translation otherPos = allTranslations[allObstacles[i]];
                        var distance = Vector3.Distance(pos.Value, otherPos.Value);
                        if (distance < boid.ObstacleAversionDistance) {
                            avoidObstacle = true;
                            var obstacleSteering = pos.Value - otherPos.Value;
                            if (obstacleSteering.Equals(float3.zero))
                                obstacleSteering = movement.Heading;
                            obstacleSteering = math.normalizesafe(obstacleSteering);

                            avoidanceHeading = (otherPos.Value + obstacleSteering * boid.ObstacleAversionDistance) - pos.Value;
                            break;
                        }
                    }

                    if (neighborCnt > 0) {
                        separationVec /= neighborCnt;
                        cohesionPos /= neighborCnt;
                        alignmentVec /= neighborCnt;
                    } else {
                        cohesionPos = pos.Value; // Set the position to current position in order to create a zero vector as heading
                        alignmentVec = movement.Heading;
                    }

                    var targetSteering = math.normalizesafe(movement.Target - pos.Value) * boid.TargetWeight;
                    var alignmentSteering = math.normalizesafe(alignmentVec) * boid.AlignmentWeight;
                    var cohesionSteering = math.normalizesafe(cohesionPos - pos.Value) * boid.CohesionWeight;
                    var separationSteering = math.normalizesafe(separationVec) * boid.SeparationWeight;
                    var normalHeading = math.normalizesafe(targetSteering + alignmentSteering + cohesionSteering + separationSteering);
                    
                    var targetHeading = avoidObstacle ? avoidanceHeading : normalHeading;
                    movement.Heading = math.normalizesafe(movement.Heading + deltaTime * boid.SteeringSpeed * (targetHeading - movement.Heading));
                }).Schedule();

            allObstacles.Dispose(Dependency);
        }

        private static void SearchQuadrantNeighbors(NativeMultiHashMap<int, QuadrantData> quadrantHashMap, int key,
            Entity currentEntity, in Boid boid, in Translation pos, 
            ref int neighborCnt, ref float3 cohesionPos, ref float3 alignmentVec, ref float3 separationVec) {
            QuadrantData quadrantData;
            NativeMultiHashMapIterator<int> iterator;
            if (quadrantHashMap.TryGetFirstValue(key, out quadrantData, out iterator)) {
                do {
                    if (currentEntity == quadrantData.Entity)
                        continue;

                    var distance = Vector3.Distance(pos.Value, quadrantData.Position);
                    if (distance < boid.CellRadius) {
                        neighborCnt++;
                        separationVec +=
                            (pos.Value - quadrantData.Position) /
                            distance; // vector from other boid to this boid inversely proportional to distance
                        cohesionPos += quadrantData.Position;
                        alignmentVec += quadrantData.Rotation;
                    }
                } while (quadrantHashMap.TryGetNextValue(out quadrantData, ref iterator));
            }
        }
    }
}

