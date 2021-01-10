using System;
using Unity.Entities;


namespace Assets.Scripts.Spaceship.Flocking {
    /// <summary>
    /// Component holding data about the boid configuration used in flocking simulation.
    /// </summary>
    [GenerateAuthoringComponent]
    public struct BoidComponent : IComponentData {
        // This should be a shared component, however currently
        // there is an issue in entity package where AddSharedCompponent
        // does not support Burst and cannot be used in buffer system
        // as it is used in my ShipSpawnerSystem...
        [NonSerialized] public float CellRadius;
        [NonSerialized] public float SeparationWeight;
        [NonSerialized] public float AlignmentWeight;
        [NonSerialized] public float CohesionWeight;
        [NonSerialized] public float TargetWeight;
        [NonSerialized] public float PursuitWeight;
        [NonSerialized] public float ObstacleAversionDistance;
        [NonSerialized] public float SteeringSpeed;
    }
}