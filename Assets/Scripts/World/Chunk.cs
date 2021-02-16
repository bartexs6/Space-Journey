using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Chunki */
public class Chunk 
{
    public string chunkID;
    public Vector2 chunkPosition;
    public List<GameObject> planetsInChunk = new List<GameObject>();

    public Chunk(string chunkID, Vector2 chunkPosition)
    {
        this.chunkID = chunkID;
        this.chunkPosition = chunkPosition;
    }

    public void AddPlanetToChunk(GameObject planet)
    {
        planetsInChunk.Add(planet);
    }

    public void DestroyPlanetsInChunk()
    {
        for (int i = 0; i < planetsInChunk.Count; i++)
        {
            GameObject.Destroy(planetsInChunk[i]);
            planetsInChunk.Remove(planetsInChunk[i]);
        }
    }
}
