using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class perlinGenerator : MonoBehaviour
{
    public static perlinGenerator instance = null;
    [SerializeField] int pTexSizeX, pTexSizeY;
    [SerializeField] bool randNoiseOffset;
    [SerializeField] Vector2 perlinOffset;
    [SerializeField] float noiseScale = 1f;
    [SerializeField] int pGridStepX, pGridStepY;

    [SerializeField] bool previewTexture;
    public RawImage showTex1;
    public RawImage showTex2;

    [SerializeField] GameObject block;

    public Texture2D noiseMap;
     
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        GenerateNoise(showTex1);
        GenerateNoise(showTex2);
    }

    public void GenerateNoise(RawImage tex)
    {
        if (randNoiseOffset)
        {
            perlinOffset = new Vector2 (Random.Range(0,99999), Random.Range(0, 99999));
        }

        noiseMap = new Texture2D(pTexSizeX, pTexSizeY);

        for (int x = 0; x < pTexSizeX; x++)
        {
            for (int y =  0; y < pTexSizeY; y++)
            {
                noiseMap.SetPixel(x, y, SampleNoise(x, y));
            }
        }

        noiseMap.Apply();
        tex.texture = noiseMap;
    }

    Color SampleNoise(int x, int y)
    {
        float xCo = (float)x / pTexSizeX * noiseScale + perlinOffset.x;
        float yCo = (float)y / pTexSizeY * noiseScale + perlinOffset.y;

        float sample = Mathf.PerlinNoise(xCo, yCo);
        Color perlinColour = new Color(sample, sample, sample);

        return perlinColour;
    }

    public float SampleStepped(int x, int y)
    {
        int gridStepSizeX = pTexSizeX / pGridStepX;
        int gridStepSizeY = pTexSizeY / pGridStepY;

        float sampledFloat = noiseMap.GetPixel
                   ((Mathf.FloorToInt(x * gridStepSizeX)), (Mathf.FloorToInt(y * gridStepSizeX))).grayscale;

        return sampledFloat;
    }

    public float PerlinSteppedPosition(Vector3 worldPosition)
    {
        int xToSample = Mathf.FloorToInt(worldPosition.x + pGridStepX * .5f);
        int yToSample = Mathf.FloorToInt(worldPosition.z + pGridStepY * .5f);

        xToSample = xToSample % pGridStepX;
        yToSample = yToSample % pGridStepY;

        float sampledValue = SampleStepped(xToSample, yToSample);

        return sampledValue;
    }
}
