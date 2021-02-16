using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        float shakeStrenght = 0.10f;
        // Prędkość trzęsienia
        float shakeSpeed = 0.07f;
        
        // Pozycja trzęsienia jest równa losowej liczby z zakresu shakeStrenght
        Vector3 shakePosition = new Vector2(Random.Range(-shakeStrenght, shakeStrenght), Random.Range(-shakeStrenght, shakeStrenght));
        // Przesuń kamerę do pozycji trzęsienia z prędkością trzęsienia
        LeanTween.move(gameObject, transform.position + shakePosition, shakeSpeed);
    }
}
