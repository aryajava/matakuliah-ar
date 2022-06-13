using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTerrainCollider : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TerrainCollider>().enabled = false;
    }

    void Start()
    {
        GetComponent<TerrainCollider>().enabled = true;
    }
}
