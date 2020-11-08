using Unity.Entities;
using Unity.Burst;

public class LvlUpSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref LevelComponent levelComponent) =>
        {
            levelComponent.level += 1f * Time.DeltaTime;
        });
    }
}

