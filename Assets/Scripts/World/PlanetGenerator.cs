using UnityEngine;

public class PlanetGenerator
{
    public static Planet GeneratePlanetStats(PlanetType planetType)
    {
        Planet newPlanet = new Planet();

        switch (planetType)
        {
            case PlanetType.Ice:
                {
                    newPlanet.temperature = Random.Range(-225, -50);
                    ResourcesType[] resources = new ResourcesType[Random.Range(2, 2)];

                    for (int i = 0; i < resources.Length; i++)
                    {
                        resources[i] = (ResourcesType)Random.Range(5, 7);
                    }

                    newPlanet.resources = resources;
                    newPlanet.age = Random.Range(1, 120);

                    return newPlanet;
                }

            case PlanetType.Rock:
                {
                    newPlanet.temperature = Random.Range(50, 500);
                    ResourcesType[] resources = new ResourcesType[Random.Range(2, 2)];

                    for (int i = 0; i < resources.Length; i++)
                    {
                        resources[i] = (ResourcesType)Random.Range(2, 4);
                    }

                    newPlanet.resources = resources;
                    newPlanet.age = Random.Range(1, 120);

                    return newPlanet;
                }

            case PlanetType.Terra:
                {
                    newPlanet.temperature = Random.Range(-20, 50);
                    ResourcesType[] resources = new ResourcesType[Random.Range(2, 2)];

                    for (int i = 0; i < resources.Length; i++)
                    {
                        resources[i] = (ResourcesType)Random.Range(1, 3);
                    }

                    newPlanet.resources = resources;
                    newPlanet.age = Random.Range(1, 120);

                    return newPlanet;
                }

            case PlanetType.Sweet:
                {
                    newPlanet.temperature = Random.Range(-20, 30);
                    ResourcesType[] resources = new ResourcesType[Random.Range(2, 2)];

                    for (int i = 0; i < resources.Length; i++)
                    {
                        resources[i] = (ResourcesType)Random.Range(0, 3);
                    }

                    newPlanet.resources = resources;
                    newPlanet.age = Random.Range(1, 120);

                    return newPlanet;
                }

            case PlanetType.Water:
                {
                    newPlanet.temperature = Random.Range(0, 50);
                    ResourcesType[] resources = new ResourcesType[Random.Range(2, 2)];

                    for (int i = 0; i < resources.Length; i++)
                    {
                        resources[i] = (ResourcesType)Random.Range(0, 3);
                    }

                    newPlanet.resources = resources;
                    newPlanet.age = Random.Range(1, 120);

                    return newPlanet;
                }

            case PlanetType.Dark:
                {
                    newPlanet.temperature = Random.Range(-225, 80);
                    ResourcesType[] resources = new ResourcesType[Random.Range(2, 2)];

                    for (int i = 0; i < resources.Length; i++)
                    {
                        resources[i] = (ResourcesType)Random.Range(0, 5);
                    }

                    newPlanet.resources = resources;
                    newPlanet.age = Random.Range(1, 120);

                    return newPlanet;
                }

            case PlanetType.Toxic:
                {
                    newPlanet.temperature = Random.Range(50, 80);
                    ResourcesType[] resources = new ResourcesType[Random.Range(2, 2)];

                    for (int i = 0; i < resources.Length; i++)
                    {
                        resources[i] = (ResourcesType)Random.Range(3, 5);
                    }

                    newPlanet.resources = resources;
                    newPlanet.age = Random.Range(1, 120);

                    return newPlanet;
                }

            case PlanetType.Cloud:
                {
                    newPlanet.temperature = Random.Range(-225, 80);
                    ResourcesType[] resources = new ResourcesType[Random.Range(2, 2)];

                    for (int i = 0; i < resources.Length; i++)
                    {
                        resources[i] = (ResourcesType)Random.Range(0, 5);
                    }

                    newPlanet.resources = resources;
                    newPlanet.age = Random.Range(1, 120);

                    return newPlanet;
                }

            case PlanetType.Sand:
                {
                    newPlanet.temperature = Random.Range(40, 100);
                    ResourcesType[] resources = new ResourcesType[Random.Range(2, 2)];

                    for (int i = 0; i < resources.Length; i++)
                    {
                        resources[i] = (ResourcesType)Random.Range(0, 5);
                    }

                    newPlanet.resources = resources;
                    newPlanet.age = Random.Range(1, 120);

                    return newPlanet;
                }
            default:
                Manager.crashGame("Planet type error");
                return null;
        }
    }
}
