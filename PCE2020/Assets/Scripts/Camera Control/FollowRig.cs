using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Camera_Control {
    /// <summary>
    /// Script accessing the <c>Translation</c> Component of the camera rig (entity) and
    /// and applying it to transform component of the main camera.
    /// </summary>
    /// <remarks>It makes the camera move with camera rig (entity).</remarks>
    public class FollowRig : MonoBehaviour {
        public Entity RigEntity;

        private EntityManager _entityManager;

        /// <summary>
        /// Get reference to the Entity Manager when initialized
        /// </summary>
        private void Awake() {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        /// <summary>
        /// Updates the <c>Transform</c> component of the camera based on the <c>Translation</c>
        /// component of the Camera rig (entity). 
        /// </summary>
        /// <remarks>
        /// <c>LateUpdate</c> is used to make sure, that this update is performed after all
        /// <c>Update</c> calls have been performed. This way we can be sure, the camera rig (entity)
        /// has been updated already.
        /// </remarks>
        private void LateUpdate() {
            if (RigEntity == Entity.Null) return;

            var rigPosition = _entityManager.GetComponentData<Translation>(RigEntity);
            transform.position = rigPosition.Value;
        }
    }
}
