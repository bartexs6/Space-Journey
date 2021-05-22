using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[Serializable]
public class PlanetsObjects
{
    public PlanetType planetType;
    public GameObject planetGameObject;
}

/* Manager gry */
public class Manager : MonoBehaviour
{
    [SerializeField]
    public List<PlanetsObjects> PlanetsList = new List<PlanetsObjects>();
    Dictionary<PlanetType, GameObject> Planets = new Dictionary<PlanetType, GameObject>();
    public Sprite[] Asteroids = new Sprite[3];


    //public GameObject[] Planets;
    public GameObject[] devModeObjects = new GameObject[3];
    public GameObject notification;
    public GameObject itemUI;

    // Podczas uruchomienia gry
    void Start()
    {
        // Przypisz do gry managera
        Game.setManager(this);
        // Zainicjalizuj frakcje
        FactionsControler.Initialize();
        // Stwórz pliki gry
        createFiles();
        // Załaduj świat
        World.LoadWorld();
        // Zmienne sprawdzające poprawność wykonania czynności
        bool playerCheck, enemyCheck, planetsCheck, planetCameraCheck;
        // Przypisz prefab notyfikacje do UI
        UI.notification = notification;
        // Przypisz prefab itemUi do UI
        UI.itemUIPrefab = itemUI;
        // Załaduj dane o statku gracza
        PlayerShip.Initialize();
        // Wczytuje dane o questach
        Quest.ReadQuestData();
        // Wczytuje dane o NPCach z questami
        QuestGiver.LoadNpcData();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        // Stwórz system ticków
        TickTimeManager.Create();

        // Zainicjalizuj baze danych itemów
        ItemsDatabase.Initialize();

        // Spróbuj
        try
        {
            // Znaleźć gracza o tagu Player
            Game.setPlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>());
            Game.setPlayerInventory(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>());
            playerCheck = true;
        }
        catch (Exception) // W przeciwnym razie
        {
            // Wyświetl błąd
            Debug.LogError("Couldn't find player gameobject or script. Check gameobject tag and scripts");
            playerCheck = false;
        }

        // Spróbuj
        try
        {
            // Znaleźć kamere
            Game.setPlanetCamera(GameObject.Find("PlanetCamera"));
            planetCameraCheck = true;
        }
        catch (Exception) // W przeciwnym razie
        {
            // Wyświetl błąd
            Debug.LogError("Couldn't find planet camera gameobject. Check gameobject name and scripts");
            planetCameraCheck = false;
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

        // Spróuj
        try
        {
            // Sprawdza czy wszystko zgadza się z planetami
            if (PlanetsList.Count <= 0)
            {
                throw new Exception();
            }

            foreach (var i in PlanetsList)
            {
                Planets.Add(i.planetType, i.planetGameObject);
            }

            Game.setPlanetsList(Planets);
            planetsCheck = true;
        }
        catch (Exception) // W przeciwnym razie
        {
            // Wyświetl błąd
            Debug.LogError("Couldn't find any planets in PlanetsList (Or other problem)");
            planetsCheck = false;
        }

        // Zainicjalizuj UI
        bool uiCheck = UI.Initialize() && UiTerminal.GetUIElements();

        // Jeżeli coś zawiodło
        if (!playerCheck || !uiCheck || !enemyCheck || !planetsCheck || !planetCameraCheck)
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

        // Załaduj pierwszy chunk
        World.LoadChunk(new Vector2(0, 0));

    }

    public static void crashGame(string message)
    {
        UI.createErrorUI(message);
        Time.timeScale = 0;
        Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith(_ => exitGame());
    }

    public static void exitGame()
    {
        // Jeżeli gra jest uruchomiona w edytorze UNITY
#if UNITY_EDITOR
        // Wyłącz gre
        Debug.Break();
        EditorApplication.isPlaying = false;
#endif
        // Wyłącz gre
        Application.Quit();
    }

    void OnApplicationQuit()
    {
#if UNITY_EDITOR
        var constructor = SynchronizationContext.Current.GetType().GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(int) }, null);
        var newContext = constructor.Invoke(new object[] { Thread.CurrentThread.ManagedThreadId });
        SynchronizationContext.SetSynchronizationContext(newContext as SynchronizationContext);
#endif
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
