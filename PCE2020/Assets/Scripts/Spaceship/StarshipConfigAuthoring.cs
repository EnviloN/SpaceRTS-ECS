using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Spaceship {
    /// <summary>
    /// Authoring providing the ability to set up the configuration of starships from the editor.
    /// </summary>
    [ConverterVersion("joe", 1)]
    public class StarshipConfigAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
        // Default values have been set here after some testing
        public float cellRadius = 0.6f;
        public float pursuitRadius = 1.0f;
        public float separationWeight = 3.0f;
        public float alignmentWeight = 1.0f;
        public float cohesionWeight = 1.0f;
        public float targetWeight = 2.0f;
        public float pursuitWeight = 5.0f;
        public float obstacleAversionDistance = 1.7f;
        public float steeringSpeed = 8.0f;
        public float movementSpeed = 0.8f;

        /// <summary>
        /// Creates a StarshipConfigComponent with configured values and adds it to the given entity.
        /// </summary>
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var shipConfig = new StarshipConfigComponent {
                CellRadius = this.cellRadius,
                PursuitRadius = this.pursuitRadius,
                SeparationWeight = this.separationWeight,
                AlignmentWeight = this.alignmentWeight,
                CohesionWeight = this.cohesionWeight,
                TargetWeight = this.targetWeight,
                PursuitWeight = this.pursuitWeight,
                ObstacleAversionDistance = this.obstacleAversionDistance,
                SteeringSpeed = this.steeringSpeed,
                MovementSpeed = this.movementSpeed
            };
            dstManager.AddComponentData(entity, shipConfig);
        }
    }
}
