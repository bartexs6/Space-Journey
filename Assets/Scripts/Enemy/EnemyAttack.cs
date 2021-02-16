using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Atak przeciwnika */
class EnemyAttack : MonoBehaviour
{
    // ObjectPooler - klasa odpowiedzialna za tworzenie obiektów w grze
    ObjectPooler objPooler;

    // Miejsca z których wylatuje pocisk
    public Transform[] firePoints;

    // Prędkość strzelania i kolejny czas do strzału
    public float fireRate;
    private float nextTimeToFire;

    void Start()
    {
        // Do zmiennej objPooler przypisuje zainicjowaną klasę ObjectPooler
        objPooler = ObjectPooler.Instance;
    }

    // Funkcja odpowiedzialna za strzelenia
    public void Shooting()
    {
        // Jeżeli aktualny czas jest większy od czasu do następnego strzału
        if (Time.time >= nextTimeToFire)
        {
            // Dla każdego miesca z których wylatuje pocisk - stwórz pocisk
            for (int i = 0; i < firePoints.Length; i++)
            {
                objPooler.SpawnFromPool("EnemyLaser", firePoints[i].position, firePoints[i].rotation).GetComponent<BulletScript>().GetSender(tag, GetComponent<Enemy>().getDmg());
            }
            // Następny czas do wystrzału to aktualny czas plus czas do strzału
            nextTimeToFire = Time.time + fireRate;
        }
    }
}
