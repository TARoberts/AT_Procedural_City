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

        clone.transform.SetParent(this.transform);

        return heightOffset;
    }
}
