using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Spaceship.Targeting {
    /// <summary>
    /// Component holding data about the current enemy target. 
    /// </summary>
    [GenerateAuthoringComponent]
    public struct TargetingComponent : IComponentData {
        [NonSerialized] public float3 TargetPosition;
        [NonSerialized] public float TargetingRadius;
        [NonSerialized] public Entity TargetEntity;
        [NonSerialized] public bool TargetLocked;
    }
}
