using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct CameraMoveComponent : IComponentData
{
    public float3 direction;
    public float speed;
    public float fastSpeed;
    public float zoom;

    public bool useFastSpeed;
}
