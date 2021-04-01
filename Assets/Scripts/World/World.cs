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
       string chunkID = (System.Math.Round((int)chunk.x / (double)chunkSize) * chunkSize / 100) + "I" + (int)(System.Math.Round((int)chunk.y / (double)chunkSize) * chunkSize / 100);

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
        new Chunk(chunkID, chunk);
    }
}
