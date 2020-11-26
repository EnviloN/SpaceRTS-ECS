using Assets.Scripts.Entity_Selection.Components;
using Unity.Entities;

namespace Assets.Scripts.Entity_Selection.Systems {
    /// <summary>
    /// System handling the selection area based on selection control.
    /// </summary>
    public class SelectionSystem : SystemBase {
        /// <summary>
        /// Updates selection component based on Selection controls component.
        /// </summary>
        protected override void OnUpdate() {
            Entities.ForEach((ref SelectionComponent selection, in SelectionControlsComponent controls) => {
                selection.IsActive = false;

                if (controls.SelectPerformed) {
                    // Selection button pressed
                    selection.StartPosition = controls.CursorPosition;
                }

                if (controls.SelectCanceled) {
                    // Selection button released
                    selection.EndPosition = controls.CursorPosition;
                    selection.IsActive = true;
                }

            }).WithoutBurst().Run();
        }
    }
}
