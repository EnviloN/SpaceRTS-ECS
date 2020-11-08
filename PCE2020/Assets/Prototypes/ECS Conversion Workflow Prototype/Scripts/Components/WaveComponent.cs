using Unity.Entities;

[GenerateAuthoringComponent]
public struct WaveComponent : IComponentData
{
    public float amplitude;
    public float xOffset;
    public float yOffset;
}
