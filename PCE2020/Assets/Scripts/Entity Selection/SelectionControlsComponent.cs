using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Entity_Selection {
    [GenerateAuthoringComponent]
    public struct SelectionControlsComponent : IComponentData {
        public float3 CursorPosition;
        public bool SelectPerformed;
        public bool SelectCanceled;
    }
}
