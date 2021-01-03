using Unity.Entities;
using UnityEngine;


namespace Assets.Scripts.Spaceship.Flocking {
    [ConverterVersion("joe", 1)]
    public class BoidTargetAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            dstManager.AddComponentData(entity, new BoidTarget());
        }
    }
}