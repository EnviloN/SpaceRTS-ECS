using Unity.Entities;

public class LvlUpSystem : SystemBase
{
    protected override void OnUpdate()
    {
        // Local variable captured in ForEach
        float dT = Time.DeltaTime;

        Entities.ForEach((ref LevelComponent levelComponent) =>
        {
            levelComponent.level += 1f * dT;
        }).ScheduleParallel();
    }
}

