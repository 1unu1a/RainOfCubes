using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private Vector2 spawnRange = new Vector2(-5f, 5f);
    [SerializeField] private float spawnHeight = 10f;

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
        GameObject cube = CubePool.Instance.GetCube();
        Vector3 position = new Vector3(
            Random.Range(spawnRange.x, spawnRange.y),
            spawnHeight,
            Random.Range(spawnRange.x, spawnRange.y)
        );
        cube.transform.position = position;
        cube.transform.rotation = Quaternion.identity;

        Rigidbody rb = cube.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = cube.AddComponent<Rigidbody>();
        }
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}