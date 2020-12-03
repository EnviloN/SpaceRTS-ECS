using Unity.Entities;
using UnityEngine;

[ConverterVersion("joe", 1)]
public class MaterialColorAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
    public Color color;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new MaterialColor { Value = (Vector4) color });
    }
}
