using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class CameraRigMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation pos, in CameraMoveComponent movement) => {
            if (movement.useFastSpeed)
                pos.Value += movement.direction * movement.fastSpeed * deltaTime;
            else
                pos.Value += movement.direction * movement.speed * deltaTime;

            pos.Value.z += movement.zoom * deltaTime;
        }).Run();
    }
}
