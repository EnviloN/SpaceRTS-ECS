using Unity.Collections;
using Unity.Entities;
using Unity.Jobs.LowLevel.Unsafe;
using Random = Unity.Mathematics.Random;

namespace Assets.Scripts.Utils {
    /// <summary>
    /// System that creates and provides a persistent <c>NativeArray</c> of random generators that for all threads.
    /// </summary>
    /// <remarks>
    /// This array is initialized once (OnCreate).
    /// </remarks>
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    class RandomSystem : ComponentSystem {
        public NativeArray<Random> RandomGenerators { get; private set; }

        /// <summary>
        /// Initializes the <c>NativeArray</c> of random generators.
        /// </summary>
        protected override void OnCreate() {
            var randomArray = new Random[JobsUtility.MaxJobThreadCount];
            var randomSeedGenerator = new System.Random();

            for (var i = 0; i < JobsUtility.MaxJobThreadCount; ++i)
                randomArray[i] = new Random((uint) randomSeedGenerator.Next());

            RandomGenerators = new NativeArray<Random>(randomArray, Allocator.Persistent);
        }

        /// <summary>
        /// Disposes the persistent array.
        /// </summary>
        protected override void OnDestroy()
            => RandomGenerators.Dispose();

        /// <summary>
        /// Empty OnUpdate method has to be "implemented" from the <c>ComponentSystem</c>
        /// </summary>
        protected override void OnUpdate() { /* Do nothing */ }
    }
}