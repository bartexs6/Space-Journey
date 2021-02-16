using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/* Świat */
public class World : MonoBehaviour
{
    // Liczba chunków
    static int chunkAmount;
    // Ścieżka do folderu świata
    static string worldFolderPath;
    // Nazwa świata
    static string worldName;
    // Rozmiar jednego chunka
    public static int chunkSize = 200;
    // Lista chunkow
    static List<Chunk> chunkList = new List<Chunk>();
    // TYMCZASOWE
    static GameObject[] Planets;
    
    // Załaduj świat
    public static void LoadWorld()
    {
        if (string.IsNullOrEmpty(worldName))
        {
            Debug.LogWarning("Name of world is null");
            worldName = "World";
        }

        string gameDocumentPath = Manager.getGameDocumentFolder(false);

        // Pobiera liste planet
        Planets = PlanetsGenerator.PlanetsList;

        // Ścieżka do folderu świata
        System.IO.DirectoryInfo worldFolder = System.IO.Directory.CreateDirectory(System.IO.Path.Combine(gameDocumentPath, worldName));

        string pathString = System.IO.Path.Combine(worldFolder.FullName, "WorldInfo.txt");

        worldFolderPath = worldFolder.FullName + "/";

        // Jeżeli nie istnieje folder to go stwórz
        if (!System.IO.File.Exists(pathString))
        {
            Debug.Log("Creating world files");
            System.IO.File.Create(pathString).Close();
            System.IO.File.WriteAllText(pathString, string.Empty);
        }
        else
        {
            //string loadedInfo = System.IO.File.ReadAllText(pathString);
            //JsonData worldData = JsonMapper.ToObject(loadedInfo);

            /*for (int i = 0; i < worldData.Count; i++)
            {
                chunkAmount = (int)worldData[i]["ChunkAmount"];
            }*/
        }
    }

    // Załaduj chunki
    public static void LoadChunk(Vector2 chunk)
    {
       // ID chunka
       string chunkID = (System.Math.Round((int)chunk.x / (double)chunkSize) * chunkSize) + "I" + (int)(System.Math.Round((int)chunk.y / (double)chunkSize) * chunkSize);

        // Sciezka do pliku chunka
        string pathString = System.IO.Path.Combine(worldFolderPath, chunkID + ".dat");
        if (!System.IO.File.Exists(pathString))
        {
            GenerateChunk(chunk, chunkID);
            SaveChunk(chunkID);
        }
        else
        {
            string loadedInfo = System.IO.File.ReadAllText(worldFolderPath + chunkID + ".dat");
        }

        string previousChunkID = string.Empty;

        Debug.Log(chunkList.Find(i => i.chunkPosition.y <= chunk.y - chunkSize*4));

        if (chunkList.Find(i => i.chunkPosition.y <= chunk.y - chunkSize * 4) != null)
        {
            Chunk temp = chunkList.Find(i => i.chunkPosition.y <= chunk.y - chunkSize * 4);
            temp.DestroyPlanetsInChunk();
            chunkList.Remove(temp);
        }

        if (chunkList.Find(i => i.chunkPosition.y >= chunk.y + chunkSize * 4) != null)
        {
            Chunk temp = chunkList.Find(i => i.chunkPosition.y >= chunk.y + chunkSize * 4);
            temp.DestroyPlanetsInChunk();
            chunkList.Remove(temp);
        }

        if (chunkList.Find(i => i.chunkPosition.x <= chunk.x - chunkSize * 4) != null)
        {
            Chunk temp = chunkList.Find(i => i.chunkPosition.x <= chunk.x - chunkSize * 4);
            temp.DestroyPlanetsInChunk();
            chunkList.Remove(temp);
        }

        if (chunkList.Find(i => i.chunkPosition.x >= chunk.x + chunkSize * 4) != null)
        {
            Chunk temp = chunkList.Find(i => i.chunkPosition.x >= chunk.x + chunkSize * 4);
            temp.DestroyPlanetsInChunk();
            chunkList.Remove(temp);
        }
        /*if (chunkList.Find(i => i.chunkID.Contains(previousChunkID)) != null)
        {
            chunkList.Find(i => i.chunkID.Contains(previousChunkID)).planetsInChunk.ForEach(i => Destroy(i));
        }*/
    }

    // Zapisz chunki
    public static void SaveChunk(string chunkID)
    {
        System.IO.File.Create(worldFolderPath + chunkID + ".dat").Close();

        System.IO.File.WriteAllText(worldFolderPath + chunkID + ".dat", "test" + chunkID);

        string loadedInfo = System.IO.File.ReadAllText(worldFolderPath + chunkID + ".dat");
        Debug.Log(loadedInfo);
    }

    // Generowanie chunkow
    static void GenerateChunk(Vector2 chunk, string chunkID)
    {
        int x = Random.Range((int)chunk.x - chunkSize, (int)chunk.x + chunkSize);
        int y = Random.Range((int)chunk.y - chunkSize, (int)chunk.y + chunkSize);

        int p = Random.Range(0, Planets.Length);

        Chunk tempChunk = new Chunk(chunkID, chunk);

        GameObject temp = Instantiate(Planets[p], new Vector2(x,y), Quaternion.identity);
        temp.name = x + "" + y;

        tempChunk.AddPlanetToChunk(temp);
        chunkList.Add(tempChunk);
    }
}
