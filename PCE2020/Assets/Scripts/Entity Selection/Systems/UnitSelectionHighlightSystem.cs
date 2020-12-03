using System.Runtime.CompilerServices;
using Assets.Scripts.Tags;
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

            // TODO: Remove this and use color specified by the team.
            if (!ColorUtility.TryParseHtmlString("#AB0000FF", out var defaultColor))
                defaultColor = new Color(0.6f, 0f, 0f, 1f);

            Entities.WithAll<SpaceshipTag>().ForEach((Entity entity, ref MaterialColor color) => {
                color.Value = allSelected.Contains(entity) ? (Vector4) Color.white : (Vector4) defaultColor;
            }).WithoutBurst().Run();
            allSelected.Dispose(Dependency);
        }
    }
}
