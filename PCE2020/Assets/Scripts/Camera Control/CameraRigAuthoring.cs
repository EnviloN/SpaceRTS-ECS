using JetBrains.Annotations;
using Unity.Entities;
using UnityEngine;


[AddComponentMenu("Custom Authoring/Camera Rig Authoring")]
public class CameraRigAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public GameObject riggedCamera;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        FollowRig followRig = riggedCamera.GetComponent<FollowRig>();

        if (followRig == null)
            followRig = riggedCamera.AddComponent<FollowRig>();

        followRig.rigEntity = entity;
    }
}
