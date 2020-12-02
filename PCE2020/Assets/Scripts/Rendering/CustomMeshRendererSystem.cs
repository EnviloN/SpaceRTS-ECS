using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Rendering {
    [ExecuteAlways]
    [AlwaysUpdateSystem]
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    class CustomMeshRendererSystem : ComponentSystem {
        override protected void OnUpdate() {
            Entities.ForEach((CustomMeshRenderer renderer, ref LocalToWorld localToWorld) => {
                Graphics.DrawMesh(renderer.Mesh, localToWorld.Value, renderer.Material, 0);
            });
        }
    }
}
