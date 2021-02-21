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
            // Update selected spaceships
            Entities.WithAll<SelectedUnitTag>()
                .ForEach(
                    (Entity entity, ref MaterialColor color, in TeamComponent team, in TargetingComponent target) => {
                        color.Value = (Vector4) Color.white;
                    }).Schedule();

            // Update spaceships that are not selected
            Entities.WithNone<SelectedUnitTag>()
                .ForEach(
                    (Entity entity, ref MaterialColor color, in TeamComponent team, in TargetingComponent target) => {
                        color.Value = team.TeamColor;
                    }).Schedule();
        }
    }
}
