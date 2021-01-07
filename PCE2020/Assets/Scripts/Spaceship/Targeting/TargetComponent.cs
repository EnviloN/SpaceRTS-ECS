using Unity.Entities;

namespace Assets.Scripts.Spaceship {
    [GenerateAuthoringComponent]
    public struct TargetComponent : IComponentData {
        public Entity TargetEntity;
    }
}
