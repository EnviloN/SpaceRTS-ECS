using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Spaceship {
    [GenerateAuthoringComponent]
    public struct UnitContolComponent : IComponentData
    {
        public float3 CursorPosition;
        public bool MoveUnits;
    }
}
