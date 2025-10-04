using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private CubeBehaviour cubePrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private Vector2 spawnRange = new(-5f, 5f);
    [SerializeField] private float spawnHeight = 10f;
    [SerializeField] private int initialPoolSize = 20;

    public static ObjectPool<CubeBehaviour> Pool { get; private set; }

    private void Awake()
    {
        Pool = new ObjectPool<CubeBehaviour>();
        Pool.Init(cubePrefab, initialPoolSize);
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnCube();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCube()
    {
        CubeBehaviour cube = Pool.Get();
        cube.transform.SetPositionAndRotation(GetRandomPosition(), Quaternion.identity);
        cube.Init();
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(spawnRange.x, spawnRange.y),
            spawnHeight,
            Random.Range(spawnRange.x, spawnRange.y)
        );
    }
}