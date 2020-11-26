using Assets.Scripts.Entity_Selection.Components;
using Unity.Entities;
using Unity.Transforms;

/// <summary>
/// TEMPORARY SYSTEM
/// </summary>
public class MoveCursorWithMouse : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, in SelectionControlsComponent selection) => {
            translation.Value = selection.CursorPosition;
        }).Schedule();
    }
}
