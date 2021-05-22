using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/* Ticki */
public static class TickTimeManager
{
    /* Klasa odpowiedzialna za system
     * czasu w grze. Ustawiony na 10 tickow
     * na sekunde.
     */

    // Ticki na sekunde. 1/0.1 = 10 tickow/s
    public const float TICK_MAX = 0.1f;

    private static GameObject timeManagerGameObject;
    private static int tick;
    private static float tickTimer;

    // Podczas pierwszego stworzenia
    public static void Create()
    {
        if (timeManagerGameObject == null)
        {
            timeManagerGameObject = GameObject.Find("GameManager");
            timeManagerGameObject.AddComponent<TickTimeManagerSystem>();
        }
    }

    // Pobierz wartość tick
    public static int GetTick()
    {
        return tick;
    }
    private class TickTimeManagerSystem : MonoBehaviour
    {
        // Podczas uruchomienia skryptu
        private void Awake()
        {
            string pathString = Path.Combine(World.worldFolderPath, "WorldInfo.txt");
            if (!File.Exists(pathString))
            {
                tick = 0;
                File.Create(World.worldFolderPath + "WorldInfo.txt").Close();
            }
            else
            {
                string loadedInfo = File.ReadAllText(World.worldFolderPath + "WorldInfo.txt");

                // Przetworz wczytane dane na JSONa
                JsonData jsonData = JsonMapper.ToObject(loadedInfo);
                jsonData["tick"] = (jsonData.ContainsKey("tick") == false) ? 0 : jsonData["tick"];
                tick = (int)jsonData["tick"];
            }
        }

        private void Update()
        {
            tickTimer += Time.deltaTime; // Time.deltaTime to czas pomiędzy załadowaniem poprzedniej i aktualnej klatki.
            if (tickTimer >= TICK_MAX)
            {
                tickTimer = tickTimer - TICK_MAX;
                tick++;
            }
        }
    }
}
