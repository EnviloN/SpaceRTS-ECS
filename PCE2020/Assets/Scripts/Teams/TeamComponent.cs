using System;
using UnityEngine;
using Unity.Entities;

namespace Assets.Scripts.Teams {
    /// <summary>
    /// Component holding data about the assigned team.
    /// </summary>
    [GenerateAuthoringComponent]
    public struct TeamComponent : IComponentData {
        [NonSerialized] public TeamEnum Team;
        [NonSerialized] public Vector4 TeamColor;

        public enum TeamEnum {
            Red = 0,
            Blue = 1
        }
    }
}
