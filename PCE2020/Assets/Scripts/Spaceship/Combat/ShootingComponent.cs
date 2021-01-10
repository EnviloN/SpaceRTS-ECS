using System;
using Unity.Entities;
using UnityEngine.Serialization;

namespace Assets.Scripts.Spaceship.Combat {
    [Serializable]
    public struct ShootingComponent : IComponentData {
        public Entity Prefab;
        public float ProjectileSpeed;
        public float AimAngle;
        public float SecondsBetweenShots;
        public float SecondsFromLastShot;
    }
}
