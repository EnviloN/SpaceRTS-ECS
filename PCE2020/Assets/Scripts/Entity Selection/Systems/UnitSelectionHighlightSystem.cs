using Assets.Scripts.Spaceship.Targeting;
using Assets.Scripts.Tags;
using Assets.Scripts.Teams;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Entity_Selection.Systems {
    /// <summary>
    /// System handling the visual highlighting of selected units.
    /// </summary>
    public class UnitSelectionHighlightSystem : SystemBase {
        protected override void OnUpdate() {
            var allSelected = GetEntityQuery(ComponentType.ReadOnly<SelectedUnitTag>())
                .ToEntityArray(Allocator.TempJob);

            Entities.WithAll<SpaceshipTag>().ForEach((Entity entity, ref MaterialColor color, in TeamComponent team, in TargetingComponent target) => {
                color.Value = allSelected.Contains(entity) ? (Vector4) Color.white : team.TeamColor;
            }).Schedule();

            allSelected.Dispose(Dependency);
        }
    }
}
