using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City
{
#pragma warning disable 414 // TYMCZASOWO
    ColonyType cityType;
    public int population;
    public int playerReputation;
    public byte health;

    public static City createCity()
    {
        return new City()
        {
            cityType = ColonyType.City,
            population = Random.Range(1, 10000),
            playerReputation = 0,
            health = 100
        };
    }
}
