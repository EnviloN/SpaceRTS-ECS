using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Camera_Control {
    /// <summary>
    /// Component holding data about the camera movement.
    /// </summary>
    [GenerateAuthoringComponent]
    public struct CameraMoveComponent : IComponentData {
        [NonSerialized] public float3 Direction;
        [NonSerialized] public float Zoom;
        public float Speed;
        public float FastSpeed;

        [NonSerialized] public bool UseFastSpeed;
    }
}
