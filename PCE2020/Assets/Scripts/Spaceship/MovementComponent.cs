using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Spaceship {
    [GenerateAuthoringComponent]
    /// <summary>
    /// Component holding data about the movement of a spaceship.
    /// </summary>
    public struct MovementComponent : IComponentData {
        public float3 Direction;
        public float Speed;
        public float TurnSpeed;
    }
}
