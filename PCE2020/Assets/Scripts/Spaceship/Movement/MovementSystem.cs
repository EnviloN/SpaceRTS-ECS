using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Spaceship.Movement {
    public class MovementSystem : SystemBase {
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;

            Entities.ForEach(
                (ref Translation pos, in MovementComponent move, in Rotation rot) => {
                    var forwardDirection = math.rotate(rot.Value, math.up());

                    pos.Value += move.Heading * move.MaxSpeed * deltaTime;
                }).Schedule();
        }
    }
}
