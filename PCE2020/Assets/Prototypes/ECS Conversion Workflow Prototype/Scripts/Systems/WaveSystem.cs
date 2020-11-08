using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using System.Runtime.CompilerServices;

public class WaveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float time = (float) Time.ElapsedTime;

        Entities.ForEach((ref Translation trans, in MoveSpeedComponent speed, in WaveComponent waveComponent) =>
        {
            float zPosition = waveComponent.amplitude * math.sin(time * speed.Value
                + trans.Value.x * waveComponent.xOffset + trans.Value.y * waveComponent.yOffset);
            trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
        }).ScheduleParallel();
    }
}
