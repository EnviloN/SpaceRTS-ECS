using UnityEngine;
using Unity.Entities;

namespace Assets.Scripts.Teams {
    public struct TeamComponent : IComponentData {
        public TeamEnum Team;
        public Vector4 TeamColor;

        public enum TeamEnum {
            Red = 0,
            Green = 1
        }
    }
}
