using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Spaceship.Targeting {
    [GenerateAuthoringComponent]
    public struct TargetComponent : IComponentData {
        public Entity TargetEntity;
        public bool TargetLocked;
    }
}
