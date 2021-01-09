using Unity.Entities;

namespace Assets.Scripts.Spaceship {
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