using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts.Spaceship.Movement {
    /// <summary>
    /// System handling movement of entities.
    /// </summary>
    public class MovementSystem : SystemBase {
        /// <summary>
        /// Handles movement of entities.
        /// </summary>
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;

            Entities.ForEach(
                (ref Translation pos, in MovementComponent move, in Rotation rot) => {
                    pos.Value += move.Heading * move.MaxSpeed * deltaTime;
                }).ScheduleParallel();
        }
    }
}
