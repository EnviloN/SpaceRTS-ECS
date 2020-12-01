using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Planets {
    [ConverterVersion("joe", 1)]
    public class ShipSpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs {
        public GameObject Prefab;
        public float spawnsPerSecond;
        public float orbitSpeed;
        public float orbitTurnSpeed;

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {
            referencedPrefabs.Add(Prefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var shipSpawner = new ShipSpawnerComponent() {
                Prefab = conversionSystem.GetPrimaryEntity(Prefab),
                SpawnPosition = transform.position,
                OrbitSpeed = orbitSpeed,
                OrbitTurnSpeed = orbitTurnSpeed,
                SecondsBetweenSpawns = 1 / spawnsPerSecond
            };
            dstManager.AddComponentData(entity, shipSpawner);
        }
    }
}
