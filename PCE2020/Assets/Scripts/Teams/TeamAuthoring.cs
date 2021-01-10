using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Teams {
    /// <summary>
    /// Authoring used for conversion of TeamComponent and custom setting od the Team.
    /// </summary>
    [ConverterVersion("joe", 1)]
    public class TeamAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
        public TeamComponent.TeamEnum team;
        public Color teamColor;

        /// <summary>
        /// Creates a new TeamComponent and adds it to a given entity.
        /// </summary>
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var teamComponent = new TeamComponent {Team = team, TeamColor = teamColor};
            dstManager.AddComponentData(entity, teamComponent);
        }
    }
}
