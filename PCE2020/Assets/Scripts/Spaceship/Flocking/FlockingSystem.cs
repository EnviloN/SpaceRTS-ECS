using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Spaceship.Flocking {
    public class FlockingSystem : SystemBase {
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;
            var allShips = GetEntityQuery(ComponentType.ReadOnly<SpaceshipTag>())
                .ToEntityArray(Allocator.TempJob);

            var allObstacles = GetEntityQuery(ComponentType.ReadOnly<BoidObstacle>())
                .ToEntityArray(Allocator.TempJob);


            Entities.WithAll<SpaceshipTag>().ForEach(
                (Entity entity, ref MovementComponent movement, in Boid boid, in Translation pos) => {
                    var neighborCnt = 0;
                    var cohesionPos = float3.zero; // For average position in neighborhood
                    var alignmentVec = float3.zero; // For average alignment vector
                    var separationVec = float3.zero; // For average alignment vector

                    var allTranslations = GetComponentDataFromEntity<Translation>(true);
                    var allLocalToWorlds = GetComponentDataFromEntity<LocalToWorld>(true);

                    // TODO: Highly inefficient - Quadtree or position hashing...
                    for (var i = 0; i < allShips.Length; i++) {
                        if (entity == allShips[i] || !allTranslations.HasComponent(allShips[i]))
                            continue;

                        var otherPos = allTranslations[allShips[i]];
                        var otherRot = allLocalToWorlds[allShips[i]];

                        var distance = Vector3.Distance(pos.Value, otherPos.Value);
                        if (distance < boid.CellRadius) {
                            neighborCnt++;
                            separationVec += (pos.Value - otherPos.Value) / distance; // vector from other boid to this boid inversely proportional to distance
                            cohesionPos += otherPos.Value;
                            alignmentVec += otherRot.Up;
                        }
                    }

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

            allShips.Dispose(Dependency);
            allObstacles.Dispose(Dependency);
        }
    }
}

