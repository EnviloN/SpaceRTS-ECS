using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

/// <summary>
/// Component holding the material color.
/// </summary>
[MaterialProperty("_StarshipColorRef", MaterialPropertyFormat.Float4)]
public struct MaterialColor : IComponentData {
    public float4 Value;
}