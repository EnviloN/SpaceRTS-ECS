using Assets.Scripts.Tags;
using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts.Entity_Selection.Systems {
    /// <summary>
    /// System handling the visual highlighting of selected units.
    /// </summary>
    public class UnitSelectionHighlightSystem : SystemBase
    {
        protected override void OnUpdate() {
            Entities.WithAll<SpaceshipTag, SelectedUnitTag>().ForEach((in Translation pos) => {
                // TODO: Highlight units
            }).Schedule();
        }
    }
}
