using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpawner : MonoBehaviour
{
    public static GridSpawner instance = null;

    public GameObject[] spawnables;
    [SerializeField] int gridX, gridY;
    [SerializeField] float gridOffset;
    [SerializeField] Vector3 gridOrigin;
    [SerializeField] RawImage perlin1, perlin2;
    public bool spawnCity = false;
    private bool citySpawned = false;



    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        gridX = Random.Range(2, 7);
        gridY = Random.Range(2, 7);
        gridOrigin = this.transform.position;
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}
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

        clone.transform.SetParent(this.gameObject.transform);
    }
    private void Update()
    {
        if (spawnCity && !citySpawned)
        {
            citySpawned = !citySpawned;
            SpawnGrid();
        }
    }
}
