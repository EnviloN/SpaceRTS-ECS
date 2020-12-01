using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Planets {
    public struct ShipSpawnerComponent : IComponentData {
        public Entity Prefab;
        public float3 SpawnPosition;
        public float OrbitSpeed;
        public float OrbitTurnSpeed;
        public float SecondsBetweenSpawns;
        public float SecondsFromLastSpawn;
    }
}
