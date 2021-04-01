using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosObj : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, new Vector2(World.chunkSize, World.chunkSize));


    }
}
