using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Spaceship {
    [ConverterVersion("joe", 1)]
    public class StarshipConfigAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
        public float CellRadius = 0.6f;
        public float PursuitRadius = 1.0f;
        public float SeparationWeight = 3.0f;
        public float AlignmentWeight = 1.0f;
        public float CohesionWeight = 1.0f;
        public float TargetWeight = 2.0f;
        public float PursuitWeight = 5.0f;
        public float ObstacleAversionDistance = 1.7f;
        public float SteeringSpeed = 8.0f;
        public float MovementSpeed = 0.8f;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var shipConfig = new StarshipConfigComponent {
                CellRadius = this.CellRadius,
                PursuitRadius = this.PursuitRadius,
                SeparationWeight = this.SeparationWeight,
                AlignmentWeight = this.AlignmentWeight,
                CohesionWeight = this.CohesionWeight,
                TargetWeight = this.TargetWeight,
                PursuitWeight = this.PursuitWeight,
                ObstacleAversionDistance = this.ObstacleAversionDistance,
                SteeringSpeed = this.SteeringSpeed,
                MovementSpeed = this.MovementSpeed
            };
            dstManager.AddComponentData(entity, shipConfig);
        }
    }
}