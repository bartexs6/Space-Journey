using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// WERSJA WSTEPNA - BRAK KOMENTARZY
public class Load : MonoBehaviour
{

    // WERSJA WSTEPNA, NWM CZY WGL PRZEJDZIE TAKI POMYSL PZDR

    void Start()
    {
        Game.HidePlayer();

        GetComponentInChildren<TMP_Text>();

        Hashtable gameStartScalePara = new Hashtable();
        gameStartScalePara.Add("onComplete", "complete");

        Invoke("animationB", 1);
        Invoke("animationA", 3);
    }

    void animationB()
    {
        LeanTween.moveLocalY(GameObject.Find("MainShip"), 50, 8);
        GameObject.Destroy(GameObject.Find("MainShip"), 8);
    }

    void animationA()
    {
        //LeanTween.scale(gameObject.transform.Find("Canvass/Galaxy").gameObject, Vector3.zero, 1).setEaseInExpo();
        LeanTweenExt.LeanAlphaText(gameObject.transform.Find("Canvass/Galaxy").GetComponent<TMP_Text>(), 0, 1);
        LeanTweenExt.LeanAlphaText(gameObject.transform.Find("Canvass/GalaxyName").GetComponent<TMP_Text>(), 0, 1);
        complete();

    }

    void complete()
    {
        Game.ShowPlayer();
    }
}
