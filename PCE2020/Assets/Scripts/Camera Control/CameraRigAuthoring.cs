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
        public float gizmoRadius = 0.5f;

        /// <summary>
        /// Method performing custom conversion steps.
        /// </summary>
        /// 
        /// <remarks>
        /// Method checks if the camera has the (MonoBehavior) component <c>FollowRig</c> and if it is missing, adds it.
        /// Then the converted entity is saved to the <c>FollowRig</c> component.
        /// </remarks>
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var followRig = riggedCamera.GetComponent<FollowRig>();

            if (followRig == null)
                followRig = riggedCamera.AddComponent<FollowRig>();

            followRig.RigEntity = entity;
        }

        /// <summary>
        /// Draw a white wire sphere in editor at the transform's position
        /// </summary>
        private void OnDrawGizmos() {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, gizmoRadius);
        }
    }
}
