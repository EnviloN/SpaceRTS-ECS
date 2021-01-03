using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Flocking {
    [ConverterVersion("joe", 1)]
    public class BoidObstacleAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            dstManager.AddComponentData(entity, new BoidObstacle());
        }
    }
}