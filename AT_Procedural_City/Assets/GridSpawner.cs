using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{

    public GameObject[] spawnables;
    [SerializeField] int gridX, gridY;
    [SerializeField] float gridOffset = 1f;
    [SerializeField] Vector3 gridOrigin = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 spawnPosition = new Vector3(x * gridOffset, 0, y * gridOffset) + gridOrigin;
                GrabObject(spawnPosition, Quaternion.identity);
            }
        }
    }

    void GrabObject(Vector3 position, Quaternion rotation)
    {
        int index = Random.Range(0, spawnables.Length);

        GameObject clone = Instantiate(spawnables[index], position, rotation);
    }
}
