using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Entity_Selection.Components {
    /// <summary>
    /// Component holding data about the selection area.
    /// </summary>
    [GenerateAuthoringComponent]
    public struct SelectionControlsComponent : IComponentData {
        [NonSerialized] public float3 CursorPosition;
        [NonSerialized] public bool SelectPerformed;
        [NonSerialized] public bool SelectCanceled;
    }
}
