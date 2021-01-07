using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Spaceship {
    public class FaceDirectionSystem : SystemBase {
        protected override void OnUpdate() {
            Entities.ForEach(
                (ref Rotation rot, in MovementComponent movement) => {
                    FaceDirection(ref rot, movement);
                }).Schedule();
        }

        private static void FaceDirection(ref Rotation rot, MovementComponent movement) {
            if (movement.Heading.Equals(float3.zero)) return;
            var targetRotation = quaternion.LookRotationSafe(math.forward(), movement.Heading);
            rot.Value = targetRotation;
        }
    }
}
