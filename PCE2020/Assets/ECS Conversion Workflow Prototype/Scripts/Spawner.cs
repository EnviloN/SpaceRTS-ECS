using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using System.Transactions;
using Unity.Transforms;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectPrefab;

    [SerializeField] private int xGrid = 10;
    [SerializeField] private int yGrid = 10;
    [Range(0.1f, 2f)]
    [SerializeField] private float ObjectSpacing = 1.05f;


    private Entity entityPrefab;
    private World defaultWorld;
    private EntityManager entityManager;

    void Start()
    {
        InitiatEntityManager();

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);
        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab, settings);

        InstantiateEtityGrid(xGrid, yGrid, ObjectSpacing);
    }

    private void InitiatEntityManager()
    {
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;
    }

    private void InstantiateEtityGrid(int dimX, int dimY, float spacing = 1f)
    {
        for (int i = 0; i < dimX; i++) {
            for (int j = 0; j < dimY; j++) {
                InstantiateEtity(new float3(i * spacing, j * spacing, 0f));
            }
        }
    }

    private void InstantiateEtity(float3 position) {
        Entity entity = entityManager.Instantiate(entityPrefab);
        entityManager.SetComponentData(entity, new Translation { Value = position });
    }
}
