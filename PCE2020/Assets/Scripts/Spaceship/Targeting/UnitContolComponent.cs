using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Spaceship.Targeting {
    /// <summary>
    /// Component holding data used for unit control.
    /// </summary>
    [GenerateAuthoringComponent]
    public struct UnitContolComponent : IComponentData {
        [NonSerialized] public float3 CursorPosition;
        [NonSerialized] public bool MoveUnits;
    }
}
