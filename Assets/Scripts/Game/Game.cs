using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/* Gra */
class Game
{
    // Zmienna przechowująca gracza
    private static Player Player;
    // Zmienna przechowująca inventory garcza
    private static PlayerInventory PlayerInventory;
    // Zmienna przechowująca EnemyControler
    private static EnemyControler EnemyControler;
    // Lista planet dostępnych w grze
    private static Dictionary<PlanetType, GameObject> PlanetsList;
    // Kamera do planet
    private static GameObject PlanetCamera;
    // Manager gry
    private static Manager Manager;

    // Przypisanie managera do zmiennej
    public static void setManager(Manager manager)
    {
        Manager = manager;
    }

    // Przypisyanie gracza do zmiennej
    public static void setPlayer(Player player)
    {
        Player = player;
    }

    // Pobierz managera
    public static Manager getGameManager()
    {
        return Manager;
    }

    // Pobieranie gracza
    public static Player getPlayer() {
        return Player;
    }

    // Przypisyanie ekwipunku do zmiennej
    public static void setPlayerInventory(PlayerInventory playerInventory)
    {
        PlayerInventory = playerInventory;
    }

    // Pobieranie ekwipunku gracza
    public static PlayerInventory getPlayerInventory()
    {
        return PlayerInventory;
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

    // Przypisyanie kamery do zmiennej
    public static void setPlanetCamera(GameObject camera)
    {
        PlanetCamera = camera;
        camera.SetActive(false);
    }
    // Pobieranie kamery
    public static GameObject getPlanetCamera()
    {
        return PlanetCamera;
    }

    // Przypisywanie planet do zmiennej
    public static void setPlanetsList(Dictionary<PlanetType, GameObject> planets)
    {
        PlanetsList = planets;
    }

    // Pobieranie ilości planet danego typu
    public static int getPlanetsTypeCount(PlanetType planetType)
    {
        return PlanetsList.Where(i => i.Key == planetType).Count();
    }

    // Pobieranie obiektu planety dostępnego w grze
    public static GameObject getPlanetPrefabsFromType(PlanetType planetType, int textureId)
    {
        return PlanetsList.Where(i => i.Key == planetType).Select(i => i.Value).ElementAt(textureId);
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
        
    // Zapisywanie gry
    public static void SaveGame()
    {
        foreach (var i in World.chunkList)
        {
            World.SaveChunk(i);
        }


        File.Create(World.worldFolderPath + "WorldInfo.txt").Close();

        var _data = new { tick = TickTimeManager.GetTick() };

        string json = JsonMapper.ToJson(_data);

        File.WriteAllText(World.worldFolderPath + "WorldInfo.txt", json);

    }
}
