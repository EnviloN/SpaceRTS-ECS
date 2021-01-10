using Assets.Scripts.Tags;
using Assets.Scripts.Teams;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Quadrants {
    /// <summary>
    /// System saving some entity data to a persistent <c>NativeMultiHashMap</c> based on the position
    /// in order to perform efficient search in other systems.
    /// </summary>
    public class QuadrantSystem : SystemBase {
        public static NativeMultiHashMap<int, QuadrantData> QuadrantHashMap;
        public const int QuadrantYMultiplier = 1000;

        private const int QuadrantCellSize = 1;

        /// <summary>
        /// Initializes an empty <c>NativeMultiHashMap</c> for quadrant system.
        /// </summary>
        /// <remarks>
        /// A persistent allocator is used, because we want this structure to be
        /// accessible and available for other systems.
        /// </remarks>
        protected override void OnCreate() {
            base.OnCreate();
            QuadrantHashMap = new NativeMultiHashMap<int, QuadrantData>(0, Allocator.Persistent);
        }

        /// <summary>
        /// Disposes the quadrant system (<c>NativeMultiHashMap</c>).
        /// </summary>
        protected override void OnDestroy() {
            base.OnDestroy();
            QuadrantHashMap.Dispose();
        }

        /// <summary>
        /// Rebuilds the quadrant system.
        /// </summary>
        protected override void OnUpdate() {
            QuadrantHashMap.Clear(); // Clear the quadrant system 

            // Add each spaceship (only some data) to the quadrant system
            Entities.WithAll<SpaceshipTag>().ForEach(
                (Entity entity, in Translation pos, in LocalToWorld ltw, in TeamComponent team) => {
                    var hasMapKey = HashKeyFromPosition(pos.Value);
                    QuadrantHashMap.Add(hasMapKey,
                        new QuadrantData {Position = pos.Value, Rotation = ltw.Up, Entity = entity, Team = team.Team});
                }).WithoutBurst().Run(); // Run on the main thread without burst compile
        }

        public static int HashKeyFromPosition(float3 pos) {
            return (int) (math.floor(pos.x / QuadrantCellSize) +
                          QuadrantYMultiplier * math.floor(pos.y / QuadrantCellSize));
        }

        /// <summary>
        /// Counts all entities in a quadrant with a given key.
        /// </summary>
        /// <param name="hashMap">The quadrant system structure (<c>NativeMultiHashMap</c>)</param>
        /// <param name="key">Key of the quadrant</param>
        /// <returns>Number of entities in the quadrant</returns>
        private static int GetEntityCountInQuadrant(NativeMultiHashMap<int, QuadrantData> hashMap, int key) {
            var count = 0;
            if (hashMap.TryGetFirstValue(key, out _, out var iterator)) {
                do {
                    count++;
                } while (hashMap.TryGetNextValue(out _, ref iterator));
            }

            return count;
        }
    }
}
