using Unity.Collections;
using Unity.Entities;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Assets.Scripts.Utils {
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    class RandomSystem : ComponentSystem {
        public NativeArray<Random> RandomGenerators { get; private set; }

        protected override void OnCreate() {
            var randomArray = new Random[JobsUtility.MaxJobThreadCount];
            var randomSeedGenerator = new System.Random();

            for (var i = 0; i < JobsUtility.MaxJobThreadCount; ++i)
                randomArray[i] = new Random((uint) randomSeedGenerator.Next());

            RandomGenerators = new NativeArray<Random>(randomArray, Allocator.Persistent);
        }

        protected override void OnDestroy()
            => RandomGenerators.Dispose();

        protected override void OnUpdate() { }
    }
}