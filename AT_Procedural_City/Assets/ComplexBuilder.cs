using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplexBuilder : MonoBehaviour
{
    public int maxPieces = 20;
    public float perlinScaleFactor = 2f;

    public int randomVariationMin = -5;
    public int randomVariationMax = 10;
    public GameObject[] baseParts;
    public GameObject[] middleParts;
    public GameObject[] topParts;

    void Start()
    {
        Build();
        int rotation = Random.Range(1, 4);

        if (rotation != 0)
        {
            if (rotation == 1)
            {
                rotation = 0;
            }

            else if (rotation == 2)
            {
                rotation = 90;
            }

            else if (rotation == 3)
            {
                rotation = 180;
            }

            else if (rotation == 4)
            {
                rotation = 270;
            }
        }


        Vector3 mid =
            new Vector3(this.gameObject.transform.position.x - .5f, this.gameObject.transform.position.y - 0.5f, this.gameObject.transform.position.z - 0.5f);

        this.gameObject.transform.RotateAround(mid, Vector3.up, rotation);
    }


    public void Build()
    {
        float sampledValue = perlinGenerator.instance.PerlinSteppedPosition(transform.position);

        int targetPieces = Mathf.FloorToInt(maxPieces * (sampledValue));
        targetPieces += Random.Range(randomVariationMin, randomVariationMax);

        if (targetPieces <= 0)
        {
            return;
        }

        float heightOffset = 0;
        heightOffset += SpawnPieceLayer(baseParts, heightOffset);

        for (int i = 2; i < targetPieces; i++)
        {
            heightOffset += SpawnPieceLayer(middleParts, heightOffset);
        }

        SpawnPieceLayer(topParts, heightOffset);
    }

    float SpawnPieceLayer(GameObject[] pieceArray, float inputHeight)
    {
        Transform randomTransform = pieceArray[Random.Range(0, pieceArray.Length)].transform;
        GameObject clone = Instantiate(randomTransform.gameObject, this.transform.position + new Vector3(0, inputHeight, 0), transform.rotation) as GameObject;
        Mesh cloneMesh = clone.GetComponentInChildren<MeshFilter>().mesh;
        Bounds baseBounds = cloneMesh.bounds;
        float heightOffset = baseBounds.size.y;

        clone.transform.SetParent(this.transform);

        return heightOffset;
    }

}
