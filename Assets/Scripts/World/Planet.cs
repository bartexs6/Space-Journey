using System.Threading.Tasks;
using UnityEngine;

public enum PlanetType { Water, Terra, Rock, Dark, Sweet, Ice, Toxic, Cloud, Sand }
public enum PlanetStatus { Uninhabitable, Empty, Inhabited, Player }
public enum ResourcesType { Hydrogen, Coal, Iron, Copper, Gold, Platinum, Plutonium, Diamond }

public class Planet
{
    private Chunk chunk;

    public string name;
    public PlanetType planetType;
    public PlanetStatus planetStatus;
    public FactionsControler.Factions planetFaction;
    public byte planetTxtID;
    public float positionX;
    public float positionY;

    public City city;

    public int age;
    public int temperature;
    public ResourcesType[] resources;

    public int experience;

    public int toExplored;
    public bool isExplored;

    public static Planet GeneratePlanet()
    {
        // Pozycja x oraz y na chunku
        int x = Random.Range(-World.chunkSize / 2, World.chunkSize / 2);
        int y = Random.Range(-World.chunkSize / 2, World.chunkSize / 2);

        int randomPlanetType = Random.Range(0, 100);
        PlanetType planetType = PlanetType.Terra;

        if (randomPlanetType <= 10)
        {
            planetType = PlanetType.Water;
        }else if(randomPlanetType > 10 && randomPlanetType <= 30)
        {
            planetType = PlanetType.Terra;
        }else if(randomPlanetType > 30 && randomPlanetType <= 40)
        {
            planetType = PlanetType.Toxic;
        }
        else if (randomPlanetType > 40 && randomPlanetType <= 45)
        {
            planetType = PlanetType.Cloud;
        }
        else if (randomPlanetType > 45 && randomPlanetType <= 55)
        {
            planetType = PlanetType.Rock;
        }
        else if (randomPlanetType > 55 && randomPlanetType <= 75)
        {
            planetType = PlanetType.Sand;
        }
        else if (randomPlanetType > 75 && randomPlanetType <= 80)
        {
            planetType = PlanetType.Dark;
        }
        else if (randomPlanetType > 80 && randomPlanetType <= 85)
        {
            planetType = PlanetType.Sweet;
        }
        else if (randomPlanetType > 85 && randomPlanetType <= 100)
        {
            planetType = PlanetType.Ice;
        }

        int randomPlanet = Random.Range(0, Game.getPlanetsTypeCount(planetType));

        Planet planetStats = PlanetGenerator.GeneratePlanetStats(planetType);

        return new Planet() {
            name = x + "" + y,
            planetType = planetType,
            planetStatus = 0,
            planetFaction = FactionsControler.Factions.Human,
            planetTxtID = (byte)randomPlanet,
            positionX = x,
            positionY = y,
            isExplored = false,

            city = null,

            age = planetStats.age,
            temperature = planetStats.temperature,
            resources = planetStats.resources,

            experience = Random.Range(50, 400)
        };
    }
    public static Planet GeneratePlanet(FactionsControler.Factions faction = FactionsControler.Factions.Human)
    {
        // Pozycja x oraz y na chunku
        int x = Random.Range(-World.chunkSize / 2, World.chunkSize / 2);
        int y = Random.Range(-World.chunkSize / 2, World.chunkSize / 2);

        int randomPlanetType = Random.Range(0, 100);
        PlanetType planetType = PlanetType.Terra;

        if (randomPlanetType <= 10)
        {
            planetType = PlanetType.Water;
        }
        else if (randomPlanetType > 10 && randomPlanetType <= 30)
        {
            planetType = PlanetType.Terra;
        }
        else if (randomPlanetType > 30 && randomPlanetType <= 40)
        {
            planetType = PlanetType.Toxic;
        }
        else if (randomPlanetType > 40 && randomPlanetType <= 45)
        {
            planetType = PlanetType.Cloud;
        }
        else if (randomPlanetType > 45 && randomPlanetType <= 55)
        {
            planetType = PlanetType.Rock;
        }
        else if (randomPlanetType > 55 && randomPlanetType <= 75)
        {
            planetType = PlanetType.Sand;
        }
        else if (randomPlanetType > 75 && randomPlanetType <= 80)
        {
            planetType = PlanetType.Dark;
        }
        else if (randomPlanetType > 80 && randomPlanetType <= 85)
        {
            planetType = PlanetType.Sweet;
        }
        else if (randomPlanetType > 85 && randomPlanetType <= 100)
        {
            planetType = PlanetType.Ice;
        }

        int randomPlanet = Random.Range(0, Game.getPlanetsTypeCount(planetType));

        Planet planetStats = PlanetGenerator.GeneratePlanetStats(planetType);

        return new Planet()
        {
            name = x + "" + y,
            planetType = planetType,
            planetStatus = (PlanetStatus)2,
            planetFaction = faction,
            planetTxtID = (byte)randomPlanet,
            positionX = x,
            positionY = y,
            isExplored = true,

            city = City.createCity(),

            age = planetStats.age,
            temperature = planetStats.temperature,
            resources = planetStats.resources,

            experience = Random.Range(50, 400)
        };
    }

    public void setChunk(Chunk chunk)
    {
        this.chunk = chunk;
    }

    public void explorePlanet()
    {
        isExplored = true;
        World.SaveChunk(chunk);
        UI.createNotification(name + " - Exploration done", 4);
    }
}
