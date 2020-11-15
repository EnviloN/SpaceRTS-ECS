using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts.Spaceship {
    public class TargetToDirectionSystem : SystemBase {
        protected override void OnUpdate() {
            Entities.WithAll<SpaceshipTag>().ForEach(
                (ref MovementComponent movement, in Translation pos, in Rotation rotation,
                    in TargetComponent target) => {
                    var allTranslations = GetComponentDataFromEntity<Translation>(true);
                    if (!allTranslations.HasComponent(target.TargetEntity))
                        return;
                    var targetPos = allTranslations[target.TargetEntity];

                    movement.Direction = targetPos.Value - pos.Value; // relative direction to target
                }).Schedule();
        }
    }
}
