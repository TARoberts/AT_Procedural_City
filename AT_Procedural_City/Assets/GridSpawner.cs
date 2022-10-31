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

    public enum CityDistrict
    {
        Center,//0
        Offices,//1
        Suburbs,//2
        Entertainment,//3
        Flats,//4
    }

    public CityDistrict district;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        int index = Random.Range(0, 4);

        switch (index)
        {
            case 0:
                district = CityDistrict.Center;
                break;

            case 1:
                district = CityDistrict.Offices;
                break;

            case 2:
                district = CityDistrict.Suburbs;
                break;

            case 3:
                district = CityDistrict.Entertainment;
                break;

            case 4:
                district = CityDistrict.Flats;
                break;
        }


        SetUpParameters();
        //gridX = Random.Range(2, 7);
        //gridY = Random.Range(2, 7);
        gridOrigin = this.transform.position;

    }

    void SetUpParameters()
    {
        switch (district)
        {
            case CityDistrict.Center:
                gridX = Random.Range(7, 8);
                gridY = Random.Range(7, 8);
                gridOffset = 1;
                break;
            case CityDistrict.Offices:

                gridX = Random.Range(4, 5);
                gridY = Random.Range(4, 5);
                gridOffset = 1.2f;
                break;

            case CityDistrict.Suburbs:

                gridX = Random.Range(5, 12);
                gridY = Random.Range(2, 2);
                gridOffset = 1;
                break;

            case CityDistrict.Entertainment:

                gridX = Random.Range(4, 4);
                gridY = Random.Range(4, 4);
                gridOffset = 2;
                break;

            case CityDistrict.Flats:

                gridX = Random.Range(1, 5);
                gridY = Random.Range(1, 12);
                gridOffset = 1;
                break;
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
        int index = Random.Range(0, spawnables.Length);

        GameObject clone = Instantiate(spawnables[index], position, rotation, this.gameObject.transform);
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
