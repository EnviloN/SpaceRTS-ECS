using Assets.Scripts.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Entity_Selection {
    public class SelectionSystem : SystemBase {

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
