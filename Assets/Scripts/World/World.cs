using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

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
    public static int chunkSize = 1000;
    // Lista chunkow
    static List<Chunk> chunkList = new List<Chunk>();
    
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
        else
        {
            string loadedInfo = File.ReadAllText(worldFolderPath + chunkID + ".dat");

            JsonData jsonData = JsonMapper.ToObject(loadedInfo);

            Planet temp = new Planet()
            {
                name = (string)jsonData[0]["name"],
                positionX = (int)jsonData[0]["positionX"],
                positionY = (int)jsonData[0]["positionY"],
                planetTxtID = (byte)jsonData[0]["planetTxtID"]
            };

            GenerateChunk(chunk, chunkID, temp);
        }
    }

    // Zapisz chunki
    public static void SaveChunk(Chunk chunk)
    {
        File.Create(worldFolderPath + chunk.chunkID + ".dat").Close();

        List<Planet> _data = new List<Planet>();
        _data.Add(chunk.planetsInChunk[0]);

        string json = JsonMapper.ToJson(_data);
        File.WriteAllText(worldFolderPath + chunk.chunkID + ".dat", json);

        string loadedInfo = System.IO.File.ReadAllText(worldFolderPath + chunk.chunkID + ".dat");
        Debug.Log(loadedInfo);
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

    static void GenerateChunk(Vector2 chunk, string chunkID, Planet planet)
    {
        if (!chunkList.Exists((i) => (i.chunkID == chunkID)))
        {
            Chunk temp = new Chunk(chunkID, chunk);
            chunkList.Add(temp);
            SaveChunk(temp);
        }
    }
}
