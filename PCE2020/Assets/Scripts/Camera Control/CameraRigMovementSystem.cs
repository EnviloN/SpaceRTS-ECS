using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts.Camera_Control {

    /// <summary>
    /// System handling the movement of the camera rig.
    /// </summary>
    public class CameraRigMovementSystem : SystemBase {

        /// <summary>
        /// Updates the position of the camera rig based on deltaTime and CameraMoveComponent.
        /// </summary>
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;

            Entities.ForEach((ref Translation pos, in CameraMoveComponent movement) => {
                if (movement.UseFastSpeed)
                    pos.Value += movement.Direction * movement.FastSpeed * deltaTime;
                else
                    pos.Value += movement.Direction * movement.Speed * deltaTime;

                pos.Value.z += movement.Zoom * deltaTime;
            }).Run(); // Run on main thread
        }
    }
}
