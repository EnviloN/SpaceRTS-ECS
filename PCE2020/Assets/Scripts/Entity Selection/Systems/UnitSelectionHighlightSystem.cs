using Assets.Scripts.Rendering;
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

            Entities.WithAll<SpaceshipTag>().ForEach((Entity entity, CustomMeshRenderer customRender) => {
                customRender.Material = allSelected.Contains(entity) ? customRender.MaterialSelected : customRender.MaterialDefault;
            }).WithoutBurst().Run();
            allSelected.Dispose(Dependency);
        }
    }
}
