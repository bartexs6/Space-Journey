using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Chunki */
public class Chunk 
{
    public string chunkID;
    public Vector2 chunkPosition;
    public List<Planet> planetsInChunk = new List<Planet>();
    GameObject chunkGameObject;

    // Konstruktor klasy Chunk
    public Chunk(string chunkID, Vector2 chunkPosition)
    {
        this.chunkID = chunkID;
        this.chunkPosition = chunkPosition;
        
        chunkGameObject = new GameObject(chunkID);
        chunkGameObject.transform.position = chunkPosition;
        chunkGameObject.AddComponent<GizmosObj>();

        CreatePlanetsInChunk();
    }

    public Chunk(string chunkID, Vector2 chunkPosition, Planet planet)
    {
        this.chunkID = chunkID;
        this.chunkPosition = chunkPosition;

        chunkGameObject = new GameObject(chunkID);
        chunkGameObject.transform.position = chunkPosition;
        chunkGameObject.AddComponent<GizmosObj>();

        LoadPlanetsToChunk(planet);
    }

    // Tworzenie planet w chunku
    void CreatePlanetsInChunk()
    {
        int x = Random.Range(0, World.chunkSize/2);
        int y = Random.Range(0, World.chunkSize/2);

        GameObject[] planets = Game.getPlanetsList();

        int randomPlanet = Random.Range(0, planets.Length);

        GameObject temp = GameObject.Instantiate(planets[randomPlanet], chunkGameObject.transform);
        temp.transform.localPosition = new Vector2(x, y);
        temp.name = x + "" + y;

        AddPlanetToChunk(temp, (byte)randomPlanet);
    }

    void LoadPlanetsToChunk(Planet planet)
    {
        GameObject[] planets = Game.getPlanetsList();

        GameObject temp = GameObject.Instantiate(planets[planet.planetTxtID], chunkGameObject.transform);
        temp.transform.localPosition = new Vector2((float)planet.positionX, (float)planet.positionY);
        temp.name = planet.name;

        AddPlanetToChunk(temp, (byte)planet.planetTxtID);
    }

    public void AddPlanetToChunk(GameObject planet, byte planetTxtID)
    {
        Planet temp = new Planet();
        temp.name = planet.name;
        temp.positionX = planet.transform.localPosition.x;
        temp.positionY = planet.transform.localPosition.y;
        temp.planetTxtID = planetTxtID;
        planetsInChunk.Add(temp);
    }
}
