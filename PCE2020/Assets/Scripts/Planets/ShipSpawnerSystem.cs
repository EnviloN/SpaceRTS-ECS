using Assets.Scripts.Spaceship;
using Assets.Scripts.Spaceship.Flocking;
using Unity.Entities;
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
            float deltaTime = (float) Time.DeltaTime;
            var ecb = _ecbSystem.CreateCommandBuffer();

            Entities
                .ForEach((Entity entity, ref ShipSpawnerComponent shipSpawner, ref BoidConfigComponent boidConfig) => {
                    shipSpawner.SecondsFromLastSpawn += deltaTime;
                    if (shipSpawner.SecondsFromLastSpawn >= shipSpawner.SecondsBetweenSpawns) {
                        shipSpawner.SecondsFromLastSpawn = 0;
                        var shipEntity = ecb.Instantiate(shipSpawner.Prefab);
                        ecb.SetComponent(shipEntity, new Translation { Value = shipSpawner.SpawnPosition });
                        ecb.SetComponent(shipEntity, new MovementComponent {
                            Speed = shipSpawner.OrbitSpeed,
                            TurnSpeed = shipSpawner.OrbitTurnSpeed
                        });
                        ecb.SetComponent(shipEntity, new TargetComponent {
                            TargetEntity = entity
                        });
                        ecb.SetComponent(shipEntity, new Boid {
                            CellRadius = boidConfig.CellRadius,
                            SeparationWeight = boidConfig.SeparationWeight,
                            AlignmentWeight = boidConfig.AlignmentWeight,
                            TargetWeight = boidConfig.TargetWeight,
                            ObstacleAversionDistance = boidConfig.ObstacleAversionDistance,
                            MoveSpeed = boidConfig.MoveSpeed
                        });
                    }
                }).Schedule();
            _ecbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
