using Assets.Scripts.Quadrants;
using Assets.Scripts.Tags;
using Assets.Scripts.Teams;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Combat {
    [UpdateAfter(typeof(QuadrantSystem))]
    public class ProjectileSystem : SystemBase {
        private EndSimulationEntityCommandBufferSystem _ecbSystem;

        protected override void OnCreate() {
            _ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;
            var quadrantHashMap = QuadrantSystem.QuadrantHashMap;
            var ecb = _ecbSystem.CreateCommandBuffer();

            Entities.WithReadOnly(quadrantHashMap).WithAll<ProjectileTag>().ForEach(
                (Entity entity, ref ProjectileComponent projectile, in Translation pos, in TeamComponent team) => {
                    projectile.SecondsToDie -= deltaTime;
                    if (projectile.SecondsToDie <= 0) {
                        ecb.DestroyEntity(entity);
                        return;
                    }

                    var hashMapKey = QuadrantSystem.HashKeyFromPosition(pos.Value);
                    if (!quadrantHashMap.TryGetFirstValue(hashMapKey, out var quadrantData, out var iterator))
                        return;

                    do {
                        if (team.Team == quadrantData.Team) // No friendly fire (includes check if entity == other entity)
                            continue;
                        // TODO add check if colided with ship

                        var distance = Vector3.Distance(pos.Value, quadrantData.Position);
                        if (distance < projectile.KillRadius) {
                            // Hit
                            ecb.DestroyEntity(entity);
                            ecb.DestroyEntity(quadrantData.Entity);
                            return;
                        }
                    } while (quadrantHashMap.TryGetNextValue(out quadrantData, ref iterator));
                }).Schedule();
            _ecbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
