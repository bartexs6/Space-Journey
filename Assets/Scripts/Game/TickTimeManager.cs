using System;
using UnityEngine;

/* Ticki */
public static class TickTimeManager
{
    /* Klasa odpowiedzialna za system
     * czasu w grze. Ustawiony na 10 tickow
     * na sekunde.
     */

    // Ticki na sekunde. 1/0.1 = 10 tickow/s
    private const float TICK_MAX = 0.1f;

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
            tick = 0;
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
