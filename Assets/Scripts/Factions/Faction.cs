using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Frakcje */
public class Faction
{
    // Nazwa frakcji
    public string name;

    // Typy frakcji pobierany z klasy FactionsControler
    public FactionsControler.Factions faction;

    // Przechowuję relacje danej frakcji do innej
    Dictionary<Faction, int> relations = new Dictionary<Faction, int>();

    // Relacje wobec gracza
    int playerRelation;

    // Konstruktor klasy
    public Faction(string name, FactionsControler.Factions faction, int playerRelation)
    {
        this.name = name;
        this.faction = faction;
        this.playerRelation = playerRelation;
    }

    // --------------------------- //
    public void checkFactions(Faction faction, int relations)
    {
        if(this != faction)
        {
            this.relations.Add(faction, relations);
            t();
        }
    }

    public void t()
    {
        foreach (var i in relations)
        {
            Debug.Log(name + " to " + i.Key.name + " - " + i.Value);
        }
    }
}
