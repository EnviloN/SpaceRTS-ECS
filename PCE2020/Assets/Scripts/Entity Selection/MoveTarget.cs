using Assets.Scripts.Entity_Selection;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class MoveTarget : SystemBase
{
    protected override void OnUpdate()
    {     
        
        Entities.ForEach((ref Translation translation, in SelectionControlsComponent selection) => {
            translation.Value = selection.CursorPosition;
        }).Schedule();
    }
}
