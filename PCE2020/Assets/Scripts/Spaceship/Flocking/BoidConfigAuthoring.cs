using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Flocking {
    [ConverterVersion("joe", 1)]
    public class BoidConfigAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
        public float CellRadius = 8.0f;
        public float SeparationWeight = 1.0f;
        public float AlignmentWeight = 1.0f;
        public float TargetWeight = 2.0f;
        public float ObstacleAversionDistance = 30.0f;
        public float MoveSpeed = 25.0f;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var boidConfig = new BoidConfigComponent {
                CellRadius = this.CellRadius,
                SeparationWeight = this.SeparationWeight,
                AlignmentWeight = this.AlignmentWeight,
                TargetWeight = this.TargetWeight,
                ObstacleAversionDistance = this.ObstacleAversionDistance,
                MoveSpeed = this.MoveSpeed
            };
            dstManager.AddComponentData(entity, boidConfig);
        }
    }
}