using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Entity_Selection.Components {
    /// <summary>
    /// Component holding data about the selection area.
    /// </summary>
    [GenerateAuthoringComponent]
    public struct SelectionComponent : IComponentData {
        [NonSerialized] public float3 StartPosition;
        [NonSerialized] public float3 EndPosition;
        [NonSerialized] public bool IsActive;
    }
}
