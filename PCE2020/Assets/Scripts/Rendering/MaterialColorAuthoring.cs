using Unity.Entities;
using UnityEngine;

/// <summary>
/// Authoring for custom material color conversion.
/// </summary>
[ConverterVersion("joe", 1)]
public class MaterialColorAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
    public Color color;

    /// <summary>
    /// Creates a new MaterialColor component and adds it to a given entity.
    /// </summary>
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new MaterialColor { Value = (Vector4) color });
    }
}
