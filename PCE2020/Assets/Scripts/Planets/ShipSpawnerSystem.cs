using Assets.Scripts.Spaceship;
using Assets.Scripts.Spaceship.Flocking;
using Assets.Scripts.Teams;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Planets {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public class ShipSpawnerSystem : SystemBase {
        private EndFixedStepSimulationEntityCommandBufferSystem _ecbSystem;

        protected override void OnCreate() {
            _ecbSystem = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            var ecb = _ecbSystem.CreateCommandBuffer();
            var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;

            Entities
                .WithNativeDisableParallelForRestriction(randomArray)
                .ForEach((Entity entity, int nativeThreadIndex, ref ShipSpawnerComponent shipSpawner, ref BoidConfigComponent boidConfig, ref TeamComponent team) => {
                    var random = randomArray[nativeThreadIndex];

                    shipSpawner.SecondsFromLastSpawn += deltaTime;
                    if (shipSpawner.SecondsFromLastSpawn >= shipSpawner.SecondsBetweenSpawns) {
                        shipSpawner.SecondsFromLastSpawn = 0;
                        var shipEntity = ecb.Instantiate(shipSpawner.Prefab);
                        ecb.SetComponent(shipEntity, new Translation {Value = shipSpawner.SpawnPosition});
                        ecb.SetComponent(shipEntity, new MovementComponent {
                            Heading = new float3(random.NextFloat(-1f, 1f), random.NextFloat(-1f, 1f), 0f),
                            Target = shipSpawner.SpawnPosition,
                            MaxSpeed = shipSpawner.ShipMovementSpeed
                        }) ;
                        ecb.SetComponent(shipEntity, new TeamComponent {
                            Team = team.Team,
                            TeamColor = team.TeamColor
                        });
                        ecb.SetComponent(shipEntity, new Boid {
                            CellRadius = boidConfig.CellRadius,
                            SeparationWeight = boidConfig.SeparationWeight,
                            AlignmentWeight = boidConfig.AlignmentWeight,
                            CohesionWeight = boidConfig.CohesionWeight,
                            TargetWeight = boidConfig.TargetWeight,
                            ObstacleAversionDistance = boidConfig.ObstacleAversionDistance,
                            SteeringSpeed = boidConfig.SteeringSpeed
                        });
                    }
                    randomArray[nativeThreadIndex] = random;
                }).Schedule();
            _ecbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
