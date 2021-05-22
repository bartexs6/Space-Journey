using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/* Kamera */
public class CameraScript : MonoBehaviour
{
    // Obiekt śledzący
    public Transform target;

    // Prędkość przejścia
    public float smoothSpeed;
    // Offset
    public Vector3 offset;


    private void FixedUpdate()
    {
        // Pozycja
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    // "Trzęsienie" się kamery
    public void CameraShake()
    {
        // Siła trzęsienia
        float shakeStrenght = 0f;
        // Prędkość trzęsienia
        float shakeSpeed = 0f;
        
        // Pozycja trzęsienia jest równa losowej liczby z zakresu shakeStrenght
        Vector3 shakePosition = new Vector2(Random.Range(-shakeStrenght, shakeStrenght), Random.Range(-shakeStrenght, shakeStrenght));
        // Przesuń kamerę do pozycji trzęsienia z prędkością trzęsienia
        transform.DOMove(transform.position + shakePosition, shakeSpeed);
        //Dot.move(gameObject, transform.position + shakePosition, shakeSpeed);
    }
}
