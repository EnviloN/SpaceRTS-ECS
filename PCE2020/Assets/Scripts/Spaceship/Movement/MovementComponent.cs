using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Spaceship.Movement {
    /// <summary>
    /// Component holding data about the movement of a spaceship.
    /// </summary>
    [GenerateAuthoringComponent]
    public struct MovementComponent : IComponentData {
        public float3 Heading;
        public float3 Target;
        public float MaxSpeed;
    }
}
