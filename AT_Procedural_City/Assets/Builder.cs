using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] int minBlocks = 3, maxBlocks = 8;

    public GameObject[] bases, middles, tops;

    // Start is called before the first frame update
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

    // Update is called once per frame
     void Build()
    {
        int noBlocks = Random.Range(minBlocks, maxBlocks);
        float heightOffSet = 0;
        heightOffSet += SpawnLayer(bases, heightOffSet);
        
        for (int i = 2; i < noBlocks; i++)
        {
            heightOffSet += SpawnLayer(middles, heightOffSet);
        }

        SpawnLayer(tops, heightOffSet);
    }

    float SpawnLayer(GameObject[] blockArray, float inputHeight)
    {


        Transform randomTransform = blockArray[Random.Range(0, blockArray.Length)].transform;
        GameObject clone = Instantiate(randomTransform.gameObject, this.transform.position + new Vector3(0, inputHeight, 0), transform.rotation) as GameObject;
        Mesh cloneMesh = clone.GetComponentInChildren<MeshFilter>().mesh;
        Bounds baseBounds = cloneMesh.bounds;
        float heightOffset = baseBounds.size.y;

        //float floRot = rotation;

       // clone.transform.Rotate(0f, floRot, 0f, Space.World);

        clone.transform.SetParent(this.transform);
        clone.transform.position = new Vector3(this.transform.position.x, clone.transform.position.y, this.transform.position.z);
        return heightOffset;
    }
}
