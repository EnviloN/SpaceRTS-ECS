using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Rendering {
    [ConverterVersion("joe", 2)]
    public class CustomMeshRendererAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
        public Mesh Mesh = null;
        public Color color = Color.white;
        public Material materialDefault = null;
        public Material materialSelected = null;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var material = new Material(materialDefault);
            material.color = color;

            dstManager.AddComponentData(entity, new CustomMeshRenderer {
                Mesh = Mesh,
                Material = material,
                MaterialDefault = materialDefault,
                MaterialSelected = materialSelected
            });
        }
    }
}
