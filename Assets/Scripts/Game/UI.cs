using System;
using UnityEngine;
using TMPro;

/* UI */
static public class UI
{
    // Tekt wyświtlający jaki przycisk kliknąć
    static public GameObject pressText;
    // Panel planety
    static public GameObject planetPanel;
    // HP
    static public TMPro.TMP_Text hpUI;
    // UI mapy
    static public GameObject mapUI;

    // Zainicjalizuj
    public static bool Initialize()
    {
        // Spróbuj
        try
        {
            // Znaleźć obiekty
            pressText = GameObject.Find("Canvas").transform.Find("Pres").gameObject;
            planetPanel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;
            mapUI = GameObject.Find("Canvas").transform.Find("Map").gameObject;
            hpUI = GameObject.Find("Canvas").transform.Find("PlayerUI/HP").GetComponent<TMP_Text>();
            return true;
        }
        catch (Exception e) // W przeciwnym razie
        {
            // Wyśiwetl błąd
            Debug.LogError("Couldn't find gameobject in UI.cs");
            Debug.LogError(e);
            return false;
        }
    }

    // Zaktualizuj tekst HP
    public static void updateHP(int hp)
    {
        hpUI.text = "HP: " + hp;
    }
}
