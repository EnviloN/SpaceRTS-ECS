using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Planets {
    /// <summary>
    /// Component holding data about the spawning process.
    /// </summary>
    public struct ShipSpawnerComponent : IComponentData {
        public Entity Prefab;
        public float3 SpawnPosition;
        public float SecondsBetweenSpawns;
        public float SecondsFromLastSpawn;
    }
}
