using Unity.Entities;

namespace Assets.Scripts.Spaceship.Combat {
    [GenerateAuthoringComponent]
    public struct ProjectileComponent : IComponentData {
        public float SecondsToDie;
        public float KillRadius;
    }
}
