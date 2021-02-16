using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// WERSJA WSTEPNA - BRAK KOMENTARZY
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("No to w droge!");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("wypierdalaj!");
    }

    public void Options()
    {
        Debug.Log("Opcje tak o");
    }
}
