using Assets.Scripts.Spaceship.Movement;
using Assets.Scripts.Tags;
using Unity.Collections;
using Unity.Entities;

namespace Assets.Scripts.Spaceship.Targeting {
    /// <summary>
    /// System handling the control of selected units.
    /// </summary>
    public class UnitControlSystem : SystemBase
    {
        // Handles unit control
        protected override void OnUpdate()
        {
            // Find all entities with UnitContolComponent (should be only one)
            var unitControlEntities = GetEntityQuery(ComponentType.ReadOnly<UnitContolComponent>())
                .ToEntityArray(Allocator.TempJob);

            if (unitControlEntities.Length != 1) {
                unitControlEntities.Dispose();
                return;
            }

            var unitControlEntity = unitControlEntities[0];
            var unitControlData = GetComponentDataFromEntity<UnitContolComponent>(true);
            // Check if units were ordered to move
            if (unitControlData[unitControlEntity].MoveUnits) {
                var position = unitControlData[unitControlEntity].CursorPosition;

                Entities.WithAll<SelectedUnitTag>().ForEach((ref MovementComponent movement) => {
                    movement.Target = position;
                }).ScheduleParallel();
            }
            
            unitControlEntities.Dispose();
        }
    }
}
