using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* Manager gry */
public class Manager : MonoBehaviour
{
    // Podczas uruchomienia gry
    void Start()
    {
        // Stwórz system ticków
        TickTimeManager.Create();
        // Zainicjalizuj frakcje
        FactionsControler.Initialize();
        // Stwórz pliki gry
        createFiles();

        // Zmienne sprawdzające poprawność wykonania czynności
        bool playerCheck, enemyCheck;

        // Spróbuj
        try
        {
            // Znaleźć gracza o tagu Player
            Game.setPlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>());
            playerCheck = true;
        }
        catch (Exception) // W przeciwnym razie
        {
            // Wyświetl błąd
            Debug.LogError("Couldn't find player gameobject. Check gameobject tag and scripts");
            playerCheck = false;
        }

        // Spróbuj
        try
        {
            // Znaleźć EnemyControler o naziwe GameManager
            Game.setEnemyControler(GameObject.Find("GameManager").GetComponent<EnemyControler>());
            enemyCheck = true;
        }
        catch (Exception)  // W przeciwnym razie
        {
            // Wyświetl błąd
            Debug.LogError("Couldn't find GameManager EnemyControle. Check gameobject name and scripts");
            enemyCheck = false;
        }

        // Zainicjalizuj UI
        bool uiCheck = UI.Initialize();

        // Jeżeli coś zawiodło
        if (!playerCheck || !uiCheck || !enemyCheck)
        {
            // Wyświetl błąd
            Debug.LogError("Game crashed");
            // Jeżeli gra jest uruchomiona w edytorze UNITY
#if UNITY_EDITOR
            // Wyłącz gre
            Debug.Break();
            EditorApplication.isPlaying = false;
#endif
            // Wyłącz gre
            Application.Quit();
        }
    }

    // Stwórz pliki
    static void createFiles()
    {
        string folderName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Space Journey";
        string pathString = System.IO.Path.Combine(folderName, "Options");

        System.IO.Directory.CreateDirectory(pathString);

        string fileName = "currentlang.txt";

        pathString = System.IO.Path.Combine(pathString, fileName);

        if (!System.IO.File.Exists(pathString))
        {
            System.IO.File.Create(pathString).Close();
            System.IO.File.WriteAllText(pathString, "en");
        }
        else
        {
            UnityEngine.Debug.Log("File already exists: " + pathString);
        }
    }
    // Pobierz scieżke do plików
    public static string getGameDocumentFolder(bool isOptionsFolder)
    {
        string folderName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Space Journey";
        string optionsFolderPath = System.IO.Path.Combine(folderName, "Options");

        if (isOptionsFolder)
        {
            return optionsFolderPath;
        }
        else
        {
            return folderName;
        }
    }

}
