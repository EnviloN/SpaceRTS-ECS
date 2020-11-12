using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Camera_Control {
    /// <summary>
    /// Authoring to make the camera rig (entity) share its reference
    /// to the <c>FollowRig</c> script running on the camera.
    /// </summary>
    [AddComponentMenu("Custom Authoring/Camera Rig Authoring")]
    public class CameraRigAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
        public GameObject riggedCamera;
        public float gizmoRadius;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var followRig = riggedCamera.GetComponent<FollowRig>();

            if (followRig == null)
                followRig = riggedCamera.AddComponent<FollowRig>();

            followRig.RigEntity = entity;
        }

        /// <summary>
        /// Draw a white wire sphere at the transform's position
        /// </summary>
        private void OnDrawGizmos() {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, gizmoRadius);
        }
    }
}
