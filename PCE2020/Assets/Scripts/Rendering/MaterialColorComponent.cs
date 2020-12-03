using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

[Serializable]
[MaterialProperty("_StarshipColorRef", MaterialPropertyFormat.Float4)]
public struct MaterialColor : IComponentData {
    public float4 Value;
}