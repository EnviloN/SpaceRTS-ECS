using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Combat {
    [ConverterVersion("joe", 1)]
    public class ShootingAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs {
        public GameObject Prefab;
        public float shotsPerSecond;
        public float projectileSpeed;
        public float aimAngle;

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {
            referencedPrefabs.Add(Prefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var shooting = new ShootingComponent {
                Prefab = conversionSystem.GetPrimaryEntity(Prefab),
                SecondsBetweenShots = 1 / shotsPerSecond,
                ProjectileSpeed = projectileSpeed,
                AimAngle = aimAngle
            };
            dstManager.AddComponentData(entity, shooting);
        }
    }
}
