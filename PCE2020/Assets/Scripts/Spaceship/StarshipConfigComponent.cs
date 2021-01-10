using Unity.Entities;

namespace Assets.Scripts.Spaceship {
    /// <summary>
    /// Component holding the configuration data from the starship creation. It is used in spawners.
    /// </summary>
    public struct StarshipConfigComponent : IComponentData {
        public float CellRadius;
        public float PursuitRadius;
        public float SeparationWeight;
        public float AlignmentWeight;
        public float CohesionWeight;
        public float TargetWeight;
        public float PursuitWeight;
        public float ObstacleAversionDistance;
        public float SteeringSpeed;
        public float MovementSpeed;
    }
}
