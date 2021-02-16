using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* UI obra�e� */
public class FloatingDamage : MonoBehaviour
{
    // Czas wy�wietlania obra�e�
    private float livingTime = 1f;
    // Mesh tesktu
    public TextMeshProUGUI txtMesh;
    // Kolor tekstu
    public Color color;

    void Start()
    {
        // Przypisz zmienne
        txtMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        txtMesh.color = color;

        // Wywo�aj funkcj� animacja
        Animation();
        // Zniszcz obiekt po okre�lonym czasie
        Destroy(gameObject, livingTime);
    }
    // Funkcjas animacji
    void Animation()
    {
        float time = 0.1f;
        float scaleChange = 2f;

        float delayTime = 0.4f;
        
        // Animacje
        LeanTween.cancel(gameObject);

        transform.localScale = Vector2.one;

        LeanTween.scale(gameObject, Vector2.one * scaleChange, time).setLoopPingPong(1).setEaseInCubic();       

        // Wywo�aj funkcj� Float po okre�lonym czasie
        Invoke("Float", delayTime + time);
    }

    // Kolejna funkcja dotycz�ca animacji
    void Float()
    {
        float yChange = 2f;
        float moveTime = 0.5f;

        LeanTween.moveY(gameObject, gameObject.transform.position.y + yChange, moveTime);
        LeanTween.scale(gameObject, Vector3.zero, moveTime).setEaseInExpo();
    }


}
