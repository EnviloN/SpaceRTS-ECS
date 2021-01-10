using Assets.Scripts.Tags;
using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts.Camera_Control {
    /// <summary>
    /// System handling the movement of the camera rig.
    /// </summary>
    public class CameraRigMovementSystem : SystemBase {
        /// <summary>
        /// Updates the position of the camera rig entity based on deltaTime and <c>CameraMoveComponent</c>.
        /// </summary>
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;

            Entities.WithAll<CameraRigTag>().ForEach((ref Translation pos, in CameraMoveComponent movement) => {
                var zoomRelativeBoost = ((-pos.Value.z) - movement.MinZoom) / (movement.MaxZoom - movement.MinZoom) + 1;

                // Camera movement on a XY plane
                if (movement.UseFastSpeed)
                    pos.Value += movement.Direction * movement.FastSpeed * zoomRelativeBoost * deltaTime;
                else
                    pos.Value += movement.Direction * movement.Speed * zoomRelativeBoost * deltaTime;

                // Camera movement on Z ax
                var zoomSpeedMultiplier = movement.UseFastSpeed ? movement.FastZoomMultiplier : 1;
                pos.Value.z += movement.Zoom * zoomSpeedMultiplier * deltaTime;

                // Note: the sign of MinZoom and MaxZoom is switched because camera moves on the negative side of Z plane
                if (pos.Value.z > -movement.MinZoom)
                    pos.Value.z = -movement.MinZoom;
                else if (pos.Value.z < -movement.MaxZoom)
                    pos.Value.z = -movement.MaxZoom;
            }).Run(); // Run on main thread
        }
    }
}
