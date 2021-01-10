using Assets.Scripts.Spaceship.Movement;
using Assets.Scripts.Spaceship.Targeting;
using Assets.Scripts.Tags;
using Assets.Scripts.Teams;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Combat {
    /// <summary>
    /// System handles starship shooting. During this process, projectile entities are instantiated.
    /// </summary>
    ///
    /// <remarks>
    /// <c>EndSimulationEntityCommandBufferSystem</c> is used for the projectile instantiation commands.
    /// </remarks>
    public class ShootingSystem : SystemBase {
        private EndSimulationEntityCommandBufferSystem _ecbSystem;

        /// <summary>
        /// Gets or creates the <c>EndSimulationEntityCommandBufferSystem</c>.
        /// </summary>
        protected override void OnCreate() {
            _ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        /// <summary>
        /// Handles the starship shooting.
        /// </summary>
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;
            var ecb = _ecbSystem.CreateCommandBuffer().AsParallelWriter();

            Entities.WithAll<SpaceshipTag>().ForEach(
                (int nativeThreadIndex, ref ShootingComponent shooting, in TargetingComponent target,
                    in Translation pos, in MovementComponent movement, in TeamComponent team) => {
                    shooting.SecondsFromLastShot += deltaTime; // Increase shooting timer

                    // Decide if it is the right moment to shoot
                    if (!target.TargetLocked || shooting.SecondsFromLastShot < shooting.SecondsBetweenShots ||
                        !ShouldShoot(movement.Heading, pos.Value, target.TargetPosition, shooting.AimAngle))
                        return;

                    shooting.SecondsFromLastShot = 0; // Reset shooting timer

                    // Instantiate a new projectile and set its components
                    var projectileEntity = ecb.Instantiate(nativeThreadIndex, shooting.Prefab);
                    ecb.SetComponent(nativeThreadIndex, projectileEntity, new Translation {Value = pos.Value});
                    ecb.SetComponent(nativeThreadIndex, projectileEntity,
                        new MovementComponent {Heading = movement.Heading, MaxSpeed = shooting.ProjectileSpeed});
                    ecb.SetComponent(nativeThreadIndex, projectileEntity,
                        new TeamComponent {Team = team.Team, TeamColor = team.TeamColor});
                }).ScheduleParallel();

            _ecbSystem.AddJobHandleForProducer(Dependency);
        }

        /// <summary>
        /// Method checks the angle between a heading vector and vector to target and returns true if is smaller than the aimAngle.
        /// </summary>
        /// <param name="heading">Starship heading vector</param>
        /// <param name="pos">Starship position</param>
        /// <param name="otherPos">Target position</param>
        /// <param name="aimAngle">Angle threshold in degrees</param>
        /// <returns>True if angle between the vectors is less then aimAngle.</returns>
        private static bool ShouldShoot(float3 heading, float3 pos, float3 otherPos, float aimAngle) {
            var vecToOther = otherPos - pos;
            var angle = Vector3.Angle(heading, vecToOther);
            return angle <= aimAngle;
        }
    }
}
