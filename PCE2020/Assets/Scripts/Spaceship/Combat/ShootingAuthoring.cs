using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Combat {
    /// <summary>
    /// Authoring used for conversion of ShootingComponent with custom configuration.
    /// It also adds the selected prefab to the referenced prefab list.
    /// </summary>
    [ConverterVersion("joe", 1)]
    public class ShootingAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs {
        public GameObject prefab; // Prefab used for projectile instantiation
        public float shotsPerSecond = 0.5f;
        public float projectileSpeed = 2f;
        public float aimAngle = 10f;

        /// <summary>
        /// Adds the projectile prefab to referenced prefabs list.
        /// </summary>
        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {
            referencedPrefabs.Add(prefab);
        }

        /// <summary>
        /// Creates a new <c>ShootingComponent</c> and adds it to a given entity.
        /// </summary>
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var shooting = new ShootingComponent {
                Prefab = conversionSystem.GetPrimaryEntity(prefab),
                SecondsBetweenShots = 1 / shotsPerSecond,
                ProjectileSpeed = projectileSpeed,
                AimAngle = aimAngle
            };
            dstManager.AddComponentData(entity, shooting);
        }
    }
}
