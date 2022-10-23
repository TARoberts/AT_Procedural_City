using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawn : MonoBehaviour
{
    public GameObject[] spawnables;
    public int gridX, gridY;
    private float gridOffset;
    public Vector3 gridOrigin = Vector3.zero;

    public bool spawnCity = false;
    private bool citySpawned = false;

    private void Awake()
    {
        if (gridX == 0)
        {
            gridX = Random.Range(2, 7);
        }
        
        if (gridY == 0)
        {
            gridY = Random.Range(2, 7);
        }
        

        if (gridX > gridY)
        {
            gridOffset = gridX*1.5f;
        }
        else if (gridX < gridY)
        {
            gridOffset = gridY*1.5f;
        }

        else
        {
            gridOffset = gridX * 1.5f;
        }
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
        int index = 0;

        GameObject clone = Instantiate(spawnables[index], position, rotation);

        clone.transform.SetParent(this.gameObject.transform);
    }

    public void ActivateSpawn()
    {
        spawnCity = true;
    }

        // Update is called once per frame
        void Update()
    {
        if (spawnCity && !citySpawned)
        {
            citySpawned = true;
            SpawnGrid();
        }
    }
}
