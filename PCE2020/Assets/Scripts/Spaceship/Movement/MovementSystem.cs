using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Spaceship {
    public class MovementSystem : SystemBase {
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;

            Entities.WithAll<SpaceshipTag>().ForEach(
                (ref Translation pos, in MovementComponent move, in Rotation rot) => {
                    var forwardDirection = math.rotate(rot.Value, math.up());

                    //pos.Value += forwardDirection * move.MaxSpeed * deltaTime;
                    pos.Value += move.Heading * move.MaxSpeed * deltaTime;
                }).Schedule();
        }
    }
}
