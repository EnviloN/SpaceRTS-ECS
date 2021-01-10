using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Spaceship.Movement {
    /// <summary>
    /// System handling rotation of entities based on their heading.
    /// </summary>
    public class FaceDirectionSystem : SystemBase {
        /// <summary>
        /// Handles rotation of entitities.
        /// </summary>
        protected override void OnUpdate() {
            Entities.ForEach(
                (ref Rotation rot, in MovementComponent movement) => {
                    FaceDirection(ref rot, movement);
                }).ScheduleParallel();
        }

        /// <summary>
        /// Method sets a rotation that corresponds to a given heading vector.
        /// </summary>
        /// <param name="rot">Reference to rotation component</param>
        /// <param name="movement">Reference to movement component</param>
        private static void FaceDirection(ref Rotation rot, MovementComponent movement) {
            if (movement.Heading.Equals(float3.zero)) return;
            var targetRotation = quaternion.LookRotationSafe(math.forward(), movement.Heading);
            rot.Value = targetRotation;
        }
    }
}
