
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        createObjectsInChunk(this);
    }

    public Chunk(string chunkID, Vector2 chunkPosition, Planet[] planets)
    {
        this.chunkID = chunkID;
        this.chunkPosition = chunkPosition;

        chunkGameObject = new GameObject(chunkID);
        chunkGameObject.transform.position = chunkPosition;
        chunkGameObject.AddComponent<GizmosObj>();

        //LoadPlanetsToChunk(planets);

        loadObjectsToChunk(planets);
    }

    void loadObjectsToChunk(object[] objects)
    {
        List<Planet> planetsTemp = new List<Planet>();
        foreach (var ob in objects)
        {
            if(ob.GetType() == typeof(Planet))
            {
                planetsTemp.Add((Planet)ob);
            }
        }

        LoadPlanetsToChunk(planetsTemp.ToArray());
    }

    void createObjectsInChunk(Chunk chunk)
    {
        CreatePlanetsInChunk(this);
    }


    // Tworzenie planet w chunku
    void CreatePlanetsInChunk(Chunk chunk)
    {
        int numberOfPlanetsInChunk = Random.Range(2, 6);

        for (int i = 0; i < numberOfPlanetsInChunk; i++)
        {
            if (System.Math.Abs(chunk.chunkPosition.x) <= 1000 && System.Math.Abs(chunk.chunkPosition.y) <= 1000)
            {
                Planet planet = Planet.GeneratePlanet(FactionsControler.Factions.Human);

                // Sprawdzanie czy istnieje planeta w poblizu tej ktora ma byc stworzona
                foreach (var p in planetsInChunk)
                {
                    if (Vector2.Distance(new Vector2(p.positionX, p.positionY), new Vector2(planet.positionX, planet.positionY)) < 20)
                    {
                        Debug.LogWarning("There is other planet on the near");
                        return;
                    }
                }

                GameObject tempg = GameObject.Instantiate(Game.getPlanetPrefabsFromType(planet.planetType, planet.planetTxtID), chunkGameObject.transform);
                tempg.transform.localPosition = new Vector2(planet.positionX, planet.positionY);

                tempg.GetComponent<PlanetGameObject>().planet = planet;
                planet.setChunk(this);

                planetsInChunk.Add(planet);
            }
            else
            {
                Planet planet = Planet.GeneratePlanet();

                // Sprawdzanie czy istnieje planeta w poblizu tej ktora ma byc stworzona
                foreach (var p in planetsInChunk)
                {
                    if (Vector2.Distance(new Vector2(p.positionX, p.positionY), new Vector2(planet.positionX, planet.positionY)) < 20)
                    {
                        Debug.LogWarning("There is other planet on the near");
                        return;
                    }
                }

                GameObject tempg = GameObject.Instantiate(Game.getPlanetPrefabsFromType(planet.planetType, planet.planetTxtID), chunkGameObject.transform);
                tempg.transform.localPosition = new Vector2(planet.positionX, planet.positionY);

                tempg.GetComponent<PlanetGameObject>().planet = planet;
                planet.setChunk(this);

                planetsInChunk.Add(planet);
            }
        }
    }

    // Zaladuj planety do chunka
    void LoadPlanetsToChunk(Planet[] planets)
    {
        foreach (var planet in planets)
        {
            GameObject temp = GameObject.Instantiate(Game.getPlanetPrefabsFromType(planet.planetType, planet.planetTxtID), chunkGameObject.transform);
            temp.transform.localPosition = new Vector2((float)planet.positionX, (float)planet.positionY);
            temp.name = planet.name;

            temp.GetComponent<PlanetGameObject>().planet = planet;
            planet.setChunk(this);

            planetsInChunk.Add(planet);
        }
    }
}
