using UnityEngine;
using System.Collections.Generic;

public class CubePool : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int poolSize = 20;

    private readonly Queue<GameObject> _pool = new Queue<GameObject>();

    public static CubePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject cube = Instantiate(cubePrefab);
            cube.SetActive(false);
            _pool.Enqueue(cube);
        }
    }

    public GameObject GetCube()
    {
        if (_pool.Count > 0)
        {
            GameObject cube = _pool.Dequeue();
            cube.SetActive(true);
            return cube;
        }
        
        return Instantiate(cubePrefab);
    }

    public void ReturnCube(GameObject cube)
    {
        cube.SetActive(false);
        _pool.Enqueue(cube);
    }
}