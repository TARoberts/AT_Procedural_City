using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpawner : MonoBehaviour
{
    public static GridSpawner instance = null;

    public GameObject[] spawnables;
    [SerializeField] int gridX = 6, gridY = 6;
    [SerializeField] float gridOffset;
    [SerializeField] Vector3 gridOrigin;
    
    [SerializeField] RawImage perlin1, perlin2;
    public bool spawnCity = false;
    private bool citySpawned = false;
    public int gridPosX, gridPosY, gridXMax, gridYMax, midX, midY;

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

        gridPosX = GetComponentInParent<CitySpawn>().x;
        gridPosY = GetComponentInParent<CitySpawn>().y;

        gridXMax = GetComponentInParent<CitySpawn>().gridX - 1;
        gridYMax = GetComponentInParent<CitySpawn>().gridY - 1;

        midX = gridXMax / 2;
        midY = gridYMax / 2;

        int index = 0;

        if (gridPosX == 0 || gridPosX == gridXMax || gridPosY == 0 || gridPosY == gridYMax)
        {
            index = 2;
        }

        else if (((gridPosX == midX - 1 || gridPosX == midX || gridPosX == midX + 1)) && ((gridPosY == midY - 1 || gridPosY == midY || gridPosY == midY + 1)))
        {
            index = 0;
        }
        else
        {
            index = Random.Range(1, 4);
            while (index == 2)
            {
                index = Random.Range(1, 4);
            }
        }
        

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


        gridOffset = 1;
        //gridX = Random.Range(2, 7);
        //gridY = Random.Range(2, 7);
        gridOrigin = this.transform.position;

    }

    //void SetUpParameters()
    //{
    //    switch (district)
    //    {
    //        case CityDistrict.Center:
    //            //gridX = Random.Range(7, 8);
    //            //gridY = Random.Range(7, 8);
    //            gridOffset = 1;
    //            break;
    //        case CityDistrict.Offices:

    //            //gridX = Random.Range(4, 5);
    //            //gridY = Random.Range(4, 5);
    //            gridOffset = 1.1f;
    //            break;

    //        case CityDistrict.Suburbs:

    //            //gridX = Random.Range(5, 12);
    //            //gridY = Random.Range(2, 2);
    //            gridOffset = 1;
    //            break;

    //        case CityDistrict.Entertainment:

    //            //gridX = Random.Range(4, 4);
    //            //gridY = Random.Range(4, 4);
    //            gridOffset = 1;
    //            break;

    //        case CityDistrict.Flats:

    //            //gridX = Random.Range(1, 5);
    //            //gridY = Random.Range(1, 12);
    //            gridOffset = 1;
    //            break;
    //    }
    //}
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
