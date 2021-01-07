using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Spaceship {
    [GenerateAuthoringComponent]
    /// <summary>
    /// Component holding data about the movement of a spaceship.
    /// </summary>
    public struct MovementComponent : IComponentData {
        public float3 Heading;
        public float3 Target;
        public float MaxSpeed;
    }
}
