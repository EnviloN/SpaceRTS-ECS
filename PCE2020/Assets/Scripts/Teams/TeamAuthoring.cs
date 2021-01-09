using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Teams {
    [ConverterVersion("joe", 1)]
    public class TeamAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
        public TeamComponent.TeamEnum team;
        public Color teamColor;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var teamComponent = new TeamComponent {
                Team = team,
                TeamColor = teamColor
            };
            dstManager.AddComponentData(entity, teamComponent);
        }
    }
}
