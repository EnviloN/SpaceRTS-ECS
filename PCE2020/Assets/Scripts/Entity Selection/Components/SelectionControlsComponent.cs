using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Entity_Selection.Components {
    /// <summary>
    /// Component holding data about the selection area.
    /// </summary>
    [GenerateAuthoringComponent]
    public struct SelectionControlsComponent : IComponentData {
        public float3 CursorPosition;
        public bool SelectPerformed;
        public bool SelectCanceled;
    }
}
