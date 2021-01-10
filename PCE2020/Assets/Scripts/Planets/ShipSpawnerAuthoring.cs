using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Planets {
    /// <summary>
    /// Authoring for conversion of <c>ShipSpawnerComponent</c>. It also adds the selected prefab to the list of referenced prefabs.
    /// </summary>
    [ConverterVersion("joe", 1)]
    public class ShipSpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs {
        public GameObject prefab; // Prefab of an object that will the spawner will create
        public float spawnsPerSecond;

        /// <summary>
        /// Adds the prefab th referenced prefabs list.
        /// </summary>
        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {
            referencedPrefabs.Add(prefab);
        }

        /// <summary>
        /// Creates a <c>ShipSpawnerComponent</c> and adds it to the given entity.
        /// </summary>
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var shipSpawner = new ShipSpawnerComponent {
                Prefab = conversionSystem.GetPrimaryEntity(prefab),
                SpawnPosition = transform.position,
                SecondsBetweenSpawns = 1 / spawnsPerSecond
            };
            dstManager.AddComponentData(entity, shipSpawner);
        }
    }
}
