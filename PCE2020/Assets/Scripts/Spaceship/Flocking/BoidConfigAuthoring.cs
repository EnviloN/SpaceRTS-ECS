using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Flocking {
    [ConverterVersion("joe", 1)]
    public class BoidConfigAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
        public float CellRadius = 0.6f;
        public float SeparationWeight = 3.0f;
        public float AlignmentWeight = 1.0f;
        public float CohesionWeight = 1.0f;
        public float TargetWeight = 2.0f;
        public float ObstacleAversionDistance = 1.7f;
        public float SteeringSpeed = 8.0f;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var boidConfig = new BoidConfigComponent {
                CellRadius = this.CellRadius,
                SeparationWeight = this.SeparationWeight,
                AlignmentWeight = this.AlignmentWeight,
                CohesionWeight = this.CohesionWeight,
                TargetWeight = this.TargetWeight,
                ObstacleAversionDistance = this.ObstacleAversionDistance,
                SteeringSpeed = this.SteeringSpeed
            };
            dstManager.AddComponentData(entity, boidConfig);
        }
    }
}