using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class FollowRig : MonoBehaviour
{
    public Entity rigEntity;
    private EntityManager entityManager;

    // Get reference to the Entity Manager when initialized
    private void Awake()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    /* Use LateUpdate to update after all Update calls. This way we can be sure, 
    the Camera Rig entity has been updated already.*/
    private void LateUpdate()
    {
        if (rigEntity == Entity.Null) return;

        Translation rigPosition = entityManager.GetComponentData<Translation>(rigEntity);
        transform.position = rigPosition.Value;
    }
}
