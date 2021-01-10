using Assets.Scripts.Quadrants;
using Assets.Scripts.Tags;
using Assets.Scripts.Teams;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Combat {
    /// <summary>
    /// System handling projectile behavior. Namely its lifespan and hit detection.
    /// </summary>
    ///
    /// <remarks>
    /// This system has to be executed after the <c>QuadrantSystem</c>, because it needs the quadrant system
    /// to be built. <c>EndSimulationEntityCommandBufferSystem</c> is used for storing entity deletion commands.
    /// </remarks>
    [UpdateAfter(typeof(QuadrantSystem))]
    public class ProjectileSystem : SystemBase {
        private EndSimulationEntityCommandBufferSystem _ecbSystem;

        /// <summary>
        /// Get or Create the <c>EndSimulationEntityCommandBufferSystem</c>.
        /// </summary>
        protected override void OnCreate() {
            _ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        /// <summary>
        /// Handles the behavior of all projectiles.
        /// </summary>
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;
            var ecb = _ecbSystem.CreateCommandBuffer().AsParallelWriter();

            // Note that we need to pass the quadrant system structure to WithReadOnly(). This is because other systems are
            // reading from it in parallel and we need to ensure the safety system that we will not modify the memory.
            var quadrantHashMap = QuadrantSystem.QuadrantHashMap;

            Entities.WithReadOnly(quadrantHashMap).WithAll<ProjectileTag>().ForEach(
                (Entity entity, int nativeThreadIndex, ref ProjectileComponent projectile, in Translation pos,
                    in TeamComponent team) => {
                    projectile.SecondsToDie -= deltaTime; // Shorten lifespan timer
                    if (projectile.SecondsToDie <= 0) {
                        ecb.DestroyEntity(nativeThreadIndex, entity); // Destroy projectile
                        return;
                    }

                    // Get hash key of projectile's position and check if there is any starship in the quadrant
                    var hashMapKey = QuadrantSystem.HashKeyFromPosition(pos.Value);
                    if (!quadrantHashMap.TryGetFirstValue(hashMapKey, out var quadrantData, out var iterator))
                        return;

                    // Iterate through the starships in the quadrant and handle potential hits.
                    do {
                        // No friendly fire (includes check if entity == other entity)
                        if (team.Team == quadrantData.Team)
                            continue;

                        var distance = Vector3.Distance(pos.Value, quadrantData.Position);
                        if (distance >= projectile.KillRadius) continue; // No hit

                        // Hit
                        ecb.DestroyEntity(nativeThreadIndex, entity); // Destroy projectile
                        ecb.DestroyEntity(nativeThreadIndex, quadrantData.Entity); // Destroy starship

                        return;
                    } while (quadrantHashMap.TryGetNextValue(out quadrantData, ref iterator));
                }).ScheduleParallel();

            _ecbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
