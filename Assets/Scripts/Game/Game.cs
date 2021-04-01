using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Gra */
class Game
{
    // Zmienna przechowująca gracza
    private static Player Player;
    // Zmienna przechowująca EnemyControler
    private static EnemyControler EnemyControler;
    // Lista planet dostępnych w grze
    private static GameObject[] PlanetsList;

    // Przypisyanie gracza do zmiennej
    public static void setPlayer(Player player)
    {
        Player = player;
    }

    // Pobieranie gracza
    public static Player getPlayer() {
        return Player;
    }

    // Przypisywanie EnemyControlera do zmiennej
    public static void setEnemyControler(EnemyControler enemyControler)
    {
        EnemyControler = enemyControler;
    }

    // Pobieranie EnemyControlera
    public static EnemyControler getEnemyControler()
    {
        return EnemyControler;
    }

    // Przypisywanie planet do zmiennej
    public static void setPlanetsList(GameObject[] planets)
    {
        PlanetsList = new GameObject[planets.Length];
        for (int i = 0; i < PlanetsList.Length; i++)
        {
            PlanetsList[i] = planets[i];
        }
    }

    // Pobieranie listy planet dostępnych w grze
    public static GameObject[] getPlanetsList()
    {
        return PlanetsList;
    }
    // Ukrywanie gracza
    public static void HidePlayer()
    {
        getPlayer().enabled = false;
        getPlayer().gameObject.SetActive(false);

        // Dla wszystkich dzieci gracza
        for (int i = 0; i < getPlayer().transform.childCount; i++)
        {
            // Wyłącz konkretne dziecko
            getPlayer().transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Pokazywanie gracza
    public static void ShowPlayer()
    {
        getPlayer().enabled = true;
        getPlayer().gameObject.SetActive(true);

        // Dla wszystkich dzieci gracza
        for (int i = 0; i < getPlayer().transform.childCount; i++)
        {
            // Wyłącz konkretne dziecko
            getPlayer().transform.GetChild(i).gameObject.SetActive(true);
        }
    }
        
}
