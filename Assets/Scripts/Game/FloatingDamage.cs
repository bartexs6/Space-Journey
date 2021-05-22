using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


/* UI obra¿eñ */
public class FloatingDamage : MonoBehaviour
{
    // Czas wyœwietlania obra¿eñ
    private float livingTime = 2f;
    // Mesh tesktu
    public TextMeshProUGUI txtMesh;
    // Kolor tekstu
    public Color color;

    void Start()
    {
        // Przypisz zmienne
        txtMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        txtMesh.color = color;

        // Wywo³aj funkcjê animacja
        Animation();
        // Zniszcz obiekt po okreœlonym czasie
        Destroy(gameObject, livingTime);
    }
    // Funkcjas animacji
    void Animation()
    {
        float time = 0.1f;
        float scaleChange = 2f;

        float delayTime = 0.4f;
        
        // Animacje
        transform.localScale = Vector2.one;

        transform.DOScale(Vector2.zero * scaleChange, time).SetLoops(1).SetEase(Ease.Flash, 15, 2);

        // Wywo³aj funkcjê Float po okreœlonym czasie
        Invoke("Float", delayTime + time);
    }

    // Kolejna funkcja dotycz¹ca animacji
    void Float()
    {
        float yChange = 2f;
        float moveTime = 0.5f;

        transform.DOMoveY(gameObject.transform.position.y + yChange, moveTime);
        transform.DOScale(Vector2.zero, moveTime);
    }


}
