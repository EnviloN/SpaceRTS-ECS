using Assets.Scripts.Spaceship.Targeting;
using Assets.Scripts.Tags;
using Assets.Scripts.Teams;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Entity_Selection.Systems {
    /// <summary>
    /// System handling the visual highlighting of selected units.
    ///
    /// If a unit is selected, it is highlighted. Otherwise, the color is set back to the team color.
    /// </summary>
    public class UnitSelectionHighlightSystem : SystemBase {
        protected override void OnUpdate() {
            // Query entities for those that have SelectedUnitTag component
            var allSelected = GetEntityQuery(ComponentType.ReadOnly<SelectedUnitTag>())
                .ToEntityArray(Allocator.TempJob);

            // Update all spaceships
            Entities.WithAll<SpaceshipTag>()
                .ForEach(
                    (Entity entity, ref MaterialColor color, in TeamComponent team, in TargetingComponent target) => {
                        color.Value = allSelected.Contains(entity) ? (Vector4) Color.white : team.TeamColor;
                    }).Schedule();

            allSelected.Dispose(Dependency);
        }
    }
}
