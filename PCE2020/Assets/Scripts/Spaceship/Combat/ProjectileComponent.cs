using Unity.Entities;

namespace Assets.Scripts.Spaceship.Combat {
    /// <summary>
    /// Component holding information about a projectile.
    /// </summary>
    [GenerateAuthoringComponent]
    public struct ProjectileComponent : IComponentData {
        public float SecondsToDie;
        public float KillRadius;
    }
}
