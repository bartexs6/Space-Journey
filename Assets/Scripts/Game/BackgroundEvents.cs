using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Do zmiany - Bez komentarzy*/
public class BackgroundEvents : MonoBehaviour
{
    // DO ZMIANY
    int EventTick;
    public GameObject asteroid;

    private void Start()
    {
        EventTick += Random.Range(1, 10);
        Asteroid();
    }


    private void Update()
    {
        if(TickTimeManager.GetTick() >= EventTick)
        {
            Asteroid();
        }
    }

    private void Asteroid()
    {
        GameObject newAsteroid = GameObject.Instantiate(asteroid, Camera.main.ViewportToWorldPoint(new Vector2(1.1f, 0.5f)), Quaternion.identity);
        newAsteroid.transform.position = new Vector3(newAsteroid.transform.position.x, newAsteroid.transform.position.y, 0);
        newAsteroid.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300, 300), Random.Range(-300, 300)), ForceMode2D.Force);
        newAsteroid.GetComponent<Rigidbody2D>().AddTorque(70);
        int asteroidSize = Random.Range(1, 3);
        newAsteroid.transform.localScale = new Vector3(asteroidSize, asteroidSize);
        newAsteroid.GetComponent<SpriteRenderer>().sortingOrder = -2;

        EventTick = TickTimeManager.GetTick() + Random.Range(100, 1000);
    }

}
