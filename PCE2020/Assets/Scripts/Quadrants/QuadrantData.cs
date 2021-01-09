using Assets.Scripts.Teams;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Quadrants {
    public struct QuadrantData {
        public float3 Position;
        public float3 Rotation;
        public Entity Entity;
        public TeamComponent.TeamEnum Team;
    }
}
