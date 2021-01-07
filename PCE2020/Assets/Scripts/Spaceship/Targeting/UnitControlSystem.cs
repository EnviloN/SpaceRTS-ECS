using Assets.Scripts.Tags;
using Unity.Collections;
using Unity.Entities;

namespace Assets.Scripts.Spaceship.Targeting {
    public class UnitControlSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var unitControlEntities = GetEntityQuery(ComponentType.ReadOnly<UnitContolComponent>())
                .ToEntityArray(Allocator.TempJob);

            if (unitControlEntities.Length != 1) {
                unitControlEntities.Dispose();
                return;
            }

            var unitControlEntity = unitControlEntities[0];
            var unitControlData = GetComponentDataFromEntity<UnitContolComponent>(true);
            if (unitControlData[unitControlEntity].MoveUnits) {
                var position = unitControlData[unitControlEntity].CursorPosition;

                Entities.WithAll<SelectedUnitTag>().ForEach((ref MovementComponent movement) => {
                    movement.Target = position;
                }).Schedule();
            }
            
            unitControlEntities.Dispose();
        }
    }
}
