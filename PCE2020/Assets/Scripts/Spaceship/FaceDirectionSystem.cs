using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Spaceship {
    public class FaceDirectionSystem : SystemBase {
        protected override void OnUpdate() {
            Entities.ForEach(
                (ref Rotation rot, in MovementComponent movement) => {
                    FaceDirection(ref rot, movement);
                }).Schedule();
        }

        private static void FaceDirection(ref Rotation rot, MovementComponent movement) {
            if (!movement.Direction.Equals(float3.zero)) {
                var targetRotation = quaternion.LookRotationSafe(math.forward(), movement.Direction);
                rot.Value = math.slerp(rot.Value, targetRotation, movement.TurnSpeed);
            }
        }
    }
}
