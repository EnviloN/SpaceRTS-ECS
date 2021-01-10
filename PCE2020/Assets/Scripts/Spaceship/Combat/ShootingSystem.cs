using Assets.Scripts.Spaceship.Targeting;
using Assets.Scripts.Teams;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Combat {
    public class ShootingSystem : SystemBase {
        private EndSimulationEntityCommandBufferSystem _ecbSystem;

        protected override void OnCreate() {
            _ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;
            var ecb = _ecbSystem.CreateCommandBuffer();

            Entities.WithAll<SpaceshipTag>().ForEach(
                        (ref ShootingComponent shooting, in TargetingComponent target, in Translation pos, in MovementComponent movement, in TeamComponent team) => {
                            shooting.SecondsFromLastShot += deltaTime;

                            if (!target.TargetLocked || shooting.SecondsFromLastShot < shooting.SecondsBetweenShots ||
                                !ShouldShoot(movement.Heading, pos.Value, target.TargetPosition, shooting.AimAngle))
                                return;

                            shooting.SecondsFromLastShot = 0;

                            var projectileEntity = ecb.Instantiate(shooting.Prefab);
                            ecb.SetComponent(projectileEntity, new Translation { Value = pos.Value });
                            ecb.SetComponent(projectileEntity, new MovementComponent {
                                Heading = movement.Heading,
                                MaxSpeed = shooting.ProjectileSpeed
                            });
                            ecb.SetComponent(projectileEntity, new TeamComponent {
                                Team = team.Team,
                                TeamColor = team.TeamColor
                            });
                        }).Schedule();
            _ecbSystem.AddJobHandleForProducer(Dependency);
        }

        private static bool ShouldShoot(float3 heading, float3 pos, float3 otherPos, float aimAngle) {
            var vecToOther = otherPos - pos;
            var angle = Vector3.Angle(heading, vecToOther);
            return angle <= aimAngle;
        }
    }
}
