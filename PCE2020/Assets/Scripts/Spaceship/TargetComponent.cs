using Unity.Entities;

[GenerateAuthoringComponent]
public struct TargetComponent : IComponentData {
    public Entity TargetEntity;
}
