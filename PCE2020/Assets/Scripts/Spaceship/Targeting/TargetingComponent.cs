using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Spaceship.Targeting {
    [GenerateAuthoringComponent]
    public struct TargetingComponent : IComponentData {
        public float3 TargetPosition;
        public float TargetingRadius;
        public Entity TargetEntity;
        public bool TargetLocked;
    }
}
