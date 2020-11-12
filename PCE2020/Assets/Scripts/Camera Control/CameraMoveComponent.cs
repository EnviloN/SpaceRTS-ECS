using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Camera_Control {
    /// <summary>
    /// Component holding data about the camera movement.
    /// </summary>
    [GenerateAuthoringComponent]
    public struct CameraMoveComponent : IComponentData {
        public float Speed;
        public float FastSpeed;
        public float FastZoomMultiplier;

        public float MinZoom;
        public float MaxZoom;

        [NonSerialized] public float3 Direction;
        [NonSerialized] public float Zoom;
        [NonSerialized] public bool UseFastSpeed;
    }
}
