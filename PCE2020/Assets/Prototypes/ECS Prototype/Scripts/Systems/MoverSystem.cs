using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class MoverSystem : SystemBase
{
    protected override void OnUpdate()
    {
        // Local variable captured in ForEach
        float dT = Time.DeltaTime;

        Entities.ForEach((ref MovementSpeedComponent movementSpeedComponent, ref Translation translation) =>
        {
            translation.Value += new float3(0, 1f * movementSpeedComponent.movementSpeed * dT, 0f);

            if (translation.Value.y > 5f || translation.Value.y < -5f)
            {
                movementSpeedComponent.movementSpeed = -movementSpeedComponent.movementSpeed; // revert speed
            }
        }).ScheduleParallel();
    }
}