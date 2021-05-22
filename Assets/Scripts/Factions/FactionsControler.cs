using System.Collections.Generic;

/* Kontroler frakcji */
public static class FactionsControler
{
    // Typy frakcji dostępne w grze
    public enum Factions
    {
        Human, Robot
    }

    // Lista frakcji
    static List<Faction> FactionList = new List<Faction>();

    // Tworzenie frakcji
    public static void Initialize()
    {
        // Dodaje nowe frakcje
        FactionList.Add(new Faction("Humans", Factions.Human, 0));
        FactionList.Add(new Faction("Robots", Factions.Robot, 0));

        // --------------------------- //
        for (int i = 0; i < FactionList.Count; i++)
        {
            for (int z = 0; z < FactionList.Count; z++)
            {
                FactionList[i].checkFactions(FactionList[z], 0);
            }
        }
    }
}
