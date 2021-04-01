using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Chunki */
public class Chunk 
{
    public string chunkID;
    public Vector2 chunkPosition;
    public List<GameObject> planetsInChunk = new List<GameObject>();
    GameObject chunkGameObject;

    // Konstruktor klasy Chunk
    public Chunk(string chunkID, Vector2 chunkPosition)
    {
        this.chunkID = chunkID;
        this.chunkPosition = chunkPosition;
        
        chunkGameObject = GameObject.Instantiate(new GameObject(chunkID, typeof(GizmosObj)), chunkPosition, Quaternion.identity);
        createPlanetsInChunk();
    }

    // Tworzenie planet w chunku
    void createPlanetsInChunk()
    {
        int x = Random.Range(0, World.chunkSize/2);
        int y = Random.Range(0, World.chunkSize/2);

        GameObject[] planets = Game.getPlanetsList();

        int randomPlanet = Random.Range(0, planets.Length);

        GameObject temp = GameObject.Instantiate(planets[randomPlanet], chunkGameObject.transform);
        temp.transform.localPosition = new Vector2(x, y);
        temp.name = x + "" + y;

        AddPlanetToChunk(temp);
    }

    public void AddPlanetToChunk(GameObject planet)
    {
        planetsInChunk.Add(planet);
    }
}
