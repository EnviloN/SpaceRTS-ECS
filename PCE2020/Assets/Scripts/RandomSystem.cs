using Unity.Mathematics;
using Unity.Entities;
using Unity.Collections;
using Unity.Jobs.LowLevel.Unsafe;

[UpdateInGroup(typeof(InitializationSystemGroup))]
class RandomSystem : ComponentSystem {
    public NativeArray<Random> RandomArray { get; private set; }

    protected override void OnCreate() {
        var randomArray = new Random[JobsUtility.MaxJobThreadCount];
        var seed = new System.Random();

        for (int i = 0; i < JobsUtility.MaxJobThreadCount; ++i)
            randomArray[i] = new Random((uint) seed.Next());

        RandomArray = new NativeArray<Random>(randomArray, Allocator.Persistent);
    }

    protected override void OnDestroy()
        => RandomArray.Dispose();

    protected override void OnUpdate() { }
}