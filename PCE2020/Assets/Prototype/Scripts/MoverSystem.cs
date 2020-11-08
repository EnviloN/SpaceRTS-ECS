using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Jobs;

public class MoverSystem : JobComponentSystem
{

    [BurstCompile]
    struct MoverSystem : IJobForEach<Position, MovementSpeed>
    {
        public float dT;

        public void Execute(ref Position Position, [ReadOnly] ref MovementSpeed movementSpeed)
        {
            float3 moveSpeed = movementSpeed.Value * dT;
            Position.Value = Position.Value + moveSpeed;
        }
    }

    /*protected override void OnUpdate()
    {
        public NativeArray<float3> positionArray = new NativeArray<float3>(Entities., Allocator.TempJob);
    MoverPallarelJob moverJob = new MoverPallarelJob
    {
        deltaTime = Time.DeltaTime,
        };
    }*/
}

/*public struct MoverPallarelJob : IJobParallelForTransform
{
    public NativeArray<float> movementSpeedArray;
    public float deltaTime;

    public void Execute(int index, TransformAccess transform)
    {
        transform.position += new Vector3(0, 1f * movementSpeedArray[index] * deltaTime, 0f);

        if (transform.position.y > 5f || transform.position.y < -5f)
        {
            movementSpeedArray[index] = -movementSpeedArray[index];
        }
    }
}*/