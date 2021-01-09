using Unity.Entities;


namespace Assets.Scripts.Spaceship.Flocking {
    [GenerateAuthoringComponent]
    public struct Boid : IComponentData {
        // This should be a shared component, however currently
        // there is an issue in entity package where AddSharedCompponent
        // does not support Burst and cannot be used in buffer system
        // as it is used in my ShipSpawnerSystem...
        public float CellRadius;
        public float SeparationWeight;
        public float AlignmentWeight;
        public float CohesionWeight;
        public float TargetWeight;
        public float PursuitWeight;
        public float ObstacleAversionDistance;
        public float SteeringSpeed;
    }
}