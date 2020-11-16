using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Entity_Selection {
    [GenerateAuthoringComponent]
    public struct SelectionComponent : IComponentData {
        public float3 StartPosition;
        public float3 EndPosition;
        public bool IsActive;
    }
}
