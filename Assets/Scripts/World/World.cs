using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

/* Świat */
public class World : MonoBehaviour
{
    // Ścieżka do folderu świata
    public static string worldFolderPath;
    // Nazwa świata
    static string worldName = "World";
    // Rozmiar jednego chunka
    public static int chunkSize = 1000;
    // Lista chunkow
    public static List<Chunk> chunkList = new List<Chunk>();

    // Załaduj świat
    public static void LoadWorld()
    {
        if (string.IsNullOrEmpty(worldName))
        {
            Debug.LogWarning("Name of world is null");
            worldName = "World";
        }

        string gameDocumentPath = Manager.getGameDocumentFolder(false);

        // Ścieżka do folderu świata
        DirectoryInfo worldFolder = Directory.CreateDirectory(Path.Combine(gameDocumentPath, worldName));

        string pathString = Path.Combine(worldFolder.FullName, "WorldInfo.txt");

        worldFolderPath = worldFolder.FullName + "/";

        // Jeżeli nie istnieje folder to go stwórz
        if (!File.Exists(pathString))
        {
            Debug.Log("Creating world files");
            File.Create(pathString).Close();
            File.WriteAllText(pathString, string.Empty);
        }
    }

    // Załaduj chunki
    public static void LoadChunk(Vector2 chunk)
    {
       // ID chunka
       string chunkID = (System.Math.Round((int)chunk.x / (double)chunkSize) * chunkSize / 100) + "I" + (int)(System.Math.Round((int)chunk.y / (double)chunkSize) * chunkSize / 100);

        // Sciezka do pliku chunka
        string pathString = Path.Combine(worldFolderPath, chunkID + ".dat");
        if (!File.Exists(pathString))
        {
            GenerateChunk(chunk, chunkID);
        }
        else if(!chunkList.Exists((i) => (i.chunkID == chunkID)))
        {
            // Wczytaj dane z pliku
            string loadedInfo = File.ReadAllText(worldFolderPath + chunkID + ".dat");

            // Przetworz wczytane dane na JSONa
            JsonData jsonData = JsonMapper.ToObject(loadedInfo);

            // Sprobuj wczytac dane do zmiennych
            try
            {
                Planet[] temp = new Planet[jsonData.Count];

                for (int i = 0; i < jsonData.Count; i++)
                {
                    ResourcesType[] resources;

                    jsonData[i]["resources"] = (jsonData[i].ContainsKey("resources") == false) ? null : jsonData[i]["resources"];

                    if(jsonData[i]["resources"] == null)
                    {
                        resources = new ResourcesType[2];
                        resources[0] = ResourcesType.Coal;
                        resources[1] = ResourcesType.Copper;
                    }
                    else
                    {
                        resources = new ResourcesType[jsonData[i]["resources"].Count];
                        for (int x = 0; x < resources.Length; x++)
                        {
                            resources[x] = (ResourcesType)(int)jsonData[i]["resources"][x];
                        }
                    }

                    // Sprawdza czy podane wartosci istnieja w pliku, jesli nie to dopisuje domyslne
                    jsonData[i]["name"] = (jsonData[i].ContainsKey("name") == false) ? "unknown" : jsonData[i]["name"];
                    jsonData[i]["positionX"] = (jsonData[i].ContainsKey("positionX") == false) ? 0 : jsonData[i]["positionX"];
                    jsonData[i]["positionY"] = (jsonData[i].ContainsKey("positionY") == false) ? 0 : jsonData[i]["positionY"];
                    jsonData[i]["planetType"] = (jsonData[i].ContainsKey("planetType") == false) ? 1 : jsonData[i]["planetType"];
                    jsonData[i]["planetStatus"] = (jsonData[i].ContainsKey("planetStatus") == false) ? 0 : jsonData[i]["planetStatus"];
                    jsonData[i]["planetFaction"] = (jsonData[i].ContainsKey("planetFaction") == false) ? 0 : jsonData[i]["planetFaction"];
                    jsonData[i]["planetTxtID"] = (jsonData[i].ContainsKey("planetTxtID") == false) ? 0 : jsonData[i]["planetTxtID"];
                    jsonData[i]["experience"] = (jsonData[i].ContainsKey("experience") == false) ? 200 : jsonData[i]["experience"];
                    jsonData[i]["toExplored"] = (jsonData[i].ContainsKey("toExplored") == false) ? 0 : jsonData[i]["toExplored"];
                    jsonData[i]["isExplored"] = (jsonData[i].ContainsKey("isExplored") == false) ? false : jsonData[i]["isExplored"];
                    jsonData[i]["age"] = (jsonData[i].ContainsKey("age") == false) ? 1 : jsonData[i]["age"];
                    jsonData[i]["city"] = (jsonData[i].ContainsKey("city") == false) ? null : jsonData[i]["city"];
                    jsonData[i]["temperature"] = (jsonData[i].ContainsKey("temperature") == false) ? 0 : jsonData[i]["temperature"];

                    Planet tempP = new Planet();
                    tempP.name = (string)jsonData[i]["name"];
                    tempP.positionX = (int)jsonData[i]["positionX"];
                    tempP.positionY = (int)jsonData[i]["positionY"];
                    tempP.planetType = (PlanetType)(int)jsonData[i]["planetType"];
                    tempP.planetStatus = (PlanetStatus)(int)jsonData[i]["planetStatus"];
                    tempP.planetFaction = (FactionsControler.Factions)(int)jsonData[0]["planetFaction"];
                    tempP.planetTxtID = (byte)jsonData[i]["planetTxtID"];
                    tempP.experience = (int)jsonData[i]["experience"];
                    if ((int)jsonData[i]["toExplored"] != 0)
                        TickJobs.Create(tempP.explorePlanet, (int)jsonData[i]["toExplored"], true);
                    tempP.isExplored = (bool)jsonData[i]["isExplored"];
                    tempP.age = (int)jsonData[i]["age"];
                    if (jsonData[i]["city"] != null)
                        tempP.city = JsonMapper.ToObject<City>(jsonData[i]["city"].ToJson());
                    tempP.temperature = (int)jsonData[i]["temperature"];
                    tempP.resources = resources;

                    temp[i] = tempP;
                }

                GenerateChunk(chunk, chunkID, temp);
            }
            catch (System.Exception e)
            {
                // Jesli cos sie nie uda wyswietl blad i wylacz gre
                Debug.LogError("Could not load world file. Remove the file and try again.");
                Debug.LogError(e);
                Manager.crashGame("Could not load world file. Remove the file and try again." + "\nError: " + e.Message + " - ChunkID: " + chunkID);
            }
        }
    }

    // Zapisz chunki
    public static void SaveChunk(Chunk chunk)
    {
        File.Create(worldFolderPath + chunk.chunkID + ".dat").Close();

        List<Planet> _data = new List<Planet>();

        foreach (var planet in chunk.planetsInChunk)
        {
            _data.Add(planet);
        }

        string json = JsonMapper.ToJson(_data);
        File.WriteAllText(worldFolderPath + chunk.chunkID + ".dat", json);

        string loadedInfo = File.ReadAllText(worldFolderPath + chunk.chunkID + ".dat");
        //Debug.Log(loadedInfo);
    }

    // Generowanie chunkow
    static void GenerateChunk(Vector2 chunk, string chunkID)
    {
        if (!chunkList.Exists((i)=>(i.chunkID == chunkID))){
            Chunk temp = new Chunk(chunkID, chunk);
            chunkList.Add(temp);
            SaveChunk(temp);
        }
    }

    // Generowanie chunkow z załadowanymi planetami
    static void GenerateChunk(Vector2 chunk, string chunkID, Planet[] planets)
    {
        if (!chunkList.Exists((i) => (i.chunkID == chunkID)))
        {
            Chunk temp = new Chunk(chunkID, chunk, planets);
            chunkList.Add(temp);
            SaveChunk(temp);
        }
    }
}
