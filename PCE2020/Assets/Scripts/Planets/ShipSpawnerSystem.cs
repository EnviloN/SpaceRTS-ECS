using Assets.Scripts.Spaceship;
using Assets.Scripts.Spaceship.Flocking;
using Assets.Scripts.Spaceship.Targeting;
using Assets.Scripts.Teams;
using Assets.Scripts.Utils;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Planets {
    /// <summary>
    /// System handling the spawning of starships.
    /// </summary>
    /// 
    /// <remarks>
    /// We want to update this system in <c>FixedStepSimulationSystemGroup</c> and add all commands to the
    /// <c>EndFixedStepSimulationEntityCommandBufferSystem</c> in order to sustain a
    /// fixed spawning frequency.
    /// </remarks>
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public class ShipSpawnerSystem : SystemBase {
        /// <summary>
        /// Reference to the <c>EndFixedStepSimulationEntityCommandBufferSystem</c> that
        /// handles buffering of entity creation commands.
        /// </summary>
        private EndFixedStepSimulationEntityCommandBufferSystem _ecbSystem;

        /// <summary>
        /// Gets or creates an <c>EndFixedStepSimulationEntityCommandBufferSystem</c>.
        /// </summary>
        protected override void OnCreate() {
            _ecbSystem = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
        }

        /// <summary>
        /// Handles starships spawning.
        /// </summary>
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;
            var ecb = _ecbSystem.CreateCommandBuffer().AsParallelWriter();

            // Get a NativeArray that contains random generators for all threads from the RandomSystem that will be
            // used to randomize the spawning. We also have to use WithNativeDisableParallelForRestriction() to
            // disable the safety system that would prevent us to create a potential Race Condition.
            // We can do this because we know that each worker will be working with one specific element
            // of the Native Array and we won't dispose the array during the job execution.
            var randomArray = World.GetExistingSystem<RandomSystem>().RandomGenerators;

            Entities.WithNativeDisableParallelForRestriction(randomArray).ForEach(
                (Entity entity, int nativeThreadIndex, ref ShipSpawnerComponent shipSpawner,
                    ref StarshipConfigComponent shipConfig, ref TeamComponent team) => {
                    var random = randomArray[nativeThreadIndex]; // Get random generator for this thread

                    shipSpawner.SecondsFromLastSpawn += deltaTime; // Increase the spawn timer
                    if (shipSpawner.SecondsFromLastSpawn >= shipSpawner.SecondsBetweenSpawns) {
                        shipSpawner.SecondsFromLastSpawn = 0; // Reset the spawn timer

                        // Instantiate an entity from the prefab and set its components
                        var shipEntity = ecb.Instantiate(nativeThreadIndex, shipSpawner.Prefab);
                        ecb.SetComponent(nativeThreadIndex, shipEntity, new Translation {Value = shipSpawner.SpawnPosition});
                        ecb.SetComponent(nativeThreadIndex, shipEntity, new MovementComponent {
                            // Randomize the heading of the spawned entity
                            Heading = new float3(random.NextFloat(-1f, 1f), random.NextFloat(-1f, 1f), 0f),
                            Target = shipSpawner.SpawnPosition,
                            MaxSpeed = shipConfig.MovementSpeed
                        });
                        ecb.SetComponent(nativeThreadIndex, shipEntity, new TeamComponent {Team = team.Team, TeamColor = team.TeamColor});
                        ecb.SetComponent(nativeThreadIndex, shipEntity,
                            new TargetingComponent() {
                                TargetingRadius = shipConfig.PursuitRadius, TargetEntity = entity, TargetLocked = false
                            });
                        ecb.SetComponent(nativeThreadIndex, shipEntity,
                            new Boid {
                                CellRadius = shipConfig.CellRadius,
                                SeparationWeight = shipConfig.SeparationWeight,
                                AlignmentWeight = shipConfig.AlignmentWeight,
                                CohesionWeight = shipConfig.CohesionWeight,
                                TargetWeight = shipConfig.TargetWeight,
                                PursuitWeight = shipConfig.PursuitWeight,
                                ObstacleAversionDistance = shipConfig.ObstacleAversionDistance,
                                SteeringSpeed = shipConfig.SteeringSpeed
                            });
                    }

                    randomArray[nativeThreadIndex] = random; // This has to be done here
                }).ScheduleParallel();

            _ecbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
