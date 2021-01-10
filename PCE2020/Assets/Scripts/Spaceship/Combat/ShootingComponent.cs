using Unity.Entities;

namespace Assets.Scripts.Spaceship.Combat {
    /// <summary>
    /// Component holding data used for projectile initialization.
    /// </summary>
    public struct ShootingComponent : IComponentData {
        public Entity Prefab;
        public float ProjectileSpeed;
        public float AimAngle;
        public float SecondsBetweenShots;
        public float SecondsFromLastShot;
    }
}
