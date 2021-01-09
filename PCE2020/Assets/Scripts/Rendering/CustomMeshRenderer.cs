using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Rendering {
    public class CustomMeshRenderer : IComponentData {
        public Mesh Mesh;
        public Material Material;
        public Material MaterialDefault;
        public Material MaterialSelected;
    }
}
