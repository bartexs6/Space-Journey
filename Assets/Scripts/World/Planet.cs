using UnityEngine;

public enum PlanetType { Water, Terra, Rock, Sand, Dark, Sweet, Ice}
public enum PlanetStatus { Uninhabitable, Empty, Inhabited }

public class Planet
{
    public string name;
    public PlanetType planetType;
    public PlanetStatus planetStatus;
    public byte planetTxtID;
    public float positionX;
    public float positionY;
}
