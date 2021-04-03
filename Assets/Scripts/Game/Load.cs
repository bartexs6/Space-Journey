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
        LeanTween.moveLocalX(GameObject.Find("Target"), 725, 1);
        LeanTween.moveLocalY(GameObject.Find("Safe"), 450, 1);
        LeanTween.size(GameObject.Find("Safe").GetComponent<RectTransform>(), new Vector2(260, 30), 1).setLoopCount(3);
        Invoke("Safe", 3);
        Invoke("Target2", 0.2f);

    }

    void Safe()
    {
        LeanTween.moveLocalY(GameObject.Find("Safe"), 1000, 2);
    }

    void Target2()
    {
        LeanTween.moveLocalX(GameObject.Find("Target2"), 725, 1);
        Invoke("TargetComplete", 5);
    }

    void TargetComplete()
    {
        LeanTween.moveLocalX(GameObject.Find("Target"), 1250, 1);
        LeanTween.moveLocalX(GameObject.Find("Target2"), 1250, 1);
    }
}
