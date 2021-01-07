using Unity.Entities;

namespace Assets.Scripts.Spaceship.Flocking {
    public struct BoidConfigComponent : IComponentData {
        public float CellRadius;
        public float SeparationWeight;
        public float AlignmentWeight;
        public float CohesionWeight;
        public float TargetWeight;
        public float ObstacleAversionDistance;
        public float SteeringSpeed;
    }
}