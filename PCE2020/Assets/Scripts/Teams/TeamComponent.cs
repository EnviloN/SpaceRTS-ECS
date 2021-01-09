using UnityEngine;
using Unity.Entities;

namespace Assets.Scripts.Teams {
    [GenerateAuthoringComponent]
    public struct TeamComponent : IComponentData {
        public TeamEnum Team;
        public Vector4 TeamColor;

        public enum TeamEnum {
            Red = 0,
            Blue = 1
        }
    }
}
