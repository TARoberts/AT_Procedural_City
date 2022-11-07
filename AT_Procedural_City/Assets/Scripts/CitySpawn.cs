using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Formats.Fbx.Exporter;
using System.IO;

public class CitySpawn : MonoBehaviour
{
    public GameObject[] spawnables;
    public int gridX, gridY;
    public float gridOffset = 5;
    public Vector3 gridOrigin = Vector3.zero;

    public GameObject roads;

    public bool spawnCity = false;
    private bool citySpawned = false;
    public int x, y;

    private void Awake()
    {

        if (gridX == 0)
        {
            gridX = 10;
        }
        
        if (gridY == 0)
        {
            gridY = 10;
        }


        //if (gridX > gridY)
        //{
        //    gridOffset = gridX*1.5f;
        //}
        //else if (gridX < gridY)
        //{
        //    gridOffset = gridY*1.5f;
        //}

        //else
        //{
        //    gridOffset = gridX * 1.5f;
        //}
    }

    void SpawnGrid()
    {
        for (x = 0; x < gridX; x++)
        {
            for (y = 0; y < gridY; y++)
            {
                Vector3 spawnPosition = new Vector3(x * gridOffset, 0, y * gridOffset) + gridOrigin;
                GrabObject(spawnPosition, Quaternion.identity);
            }
        }

        Instantiate(roads, this.transform.position, this.transform.rotation.normalized);

    }

    void GrabObject(Vector3 position, Quaternion rotation)
    {
        //int index = 0;

        GameObject clone = Instantiate(spawnables[0], position, rotation, this.gameObject.transform);
    }

    public void ActivateSpawn()
    {
        spawnCity = true;
    }

    public void ExportGameObjects()
    {
        string filePath = Path.Combine(Application.dataPath, "The City.fbx");
        ModelExporter.ExportObject(filePath, this.gameObject);

        // ModelExporter.ExportObject can be used instead of 
        // ModelExporter.ExportObjects to export a single game object
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
