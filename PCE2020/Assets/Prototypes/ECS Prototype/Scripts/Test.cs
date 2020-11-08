using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    private void Start()
    {
        // One manager to rule them all
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // Create archetype for the test entities
        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(LevelComponent),
            typeof(MovementSpeedComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(RenderBounds)
            );  

        // Create a NativeArray that will be filled with entities
        NativeArray<Entity> entityArray = new NativeArray<Entity>(200, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray); // Create entities

        // Initialize entities with their components
        foreach (Entity entity in entityArray)
        {
            entityManager.SetComponentData(entity, new LevelComponent { level = UnityEngine.Random.Range(10, 20) });
            entityManager.SetComponentData(entity, new MovementSpeedComponent { movementSpeed = UnityEngine.Random.Range(1f, 2f) });
            entityManager.SetComponentData(entity, new Translation { Value = new float3(UnityEngine.Random.Range(-12, 12f), UnityEngine.Random.Range(-4, 4f), 0) });
            entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material,
            }); ;
        }

        // The entities are created, array is now useless
        entityArray.Dispose();
    }
}
