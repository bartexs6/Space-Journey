using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DO ZMIANY - BEZ KOMENTARZY
[System.Obsolete("Uzyj generowania chunkow za pomoca World.cs.")]
public class PlanetsGenerator : MonoBehaviour
{
    public static GameObject[] PlanetsList;

    public GameObject[] planets;
    public float xRadius;
    public float yRadius;
    public int amount;

    void Start()
    {
        PlanetsList = planets;
        //GameObject _temp = Instantiate(PlanetsList[3], new Vector2(0, 10), Quaternion.identity);
        //_temp.GetComponent<Renderer>().sortingOrder = -1;

        World.LoadWorld();

        /*for (int i = 0; i < amount; i++)
        {
            float x = Random.Range(-xRadius, xRadius);
            float y = Random.Range(-yRadius, yRadius);

            Vector2 pos = new Vector2(x, y);

            int p = Random.Range(0, PlanetsList.Length);

            GameObject temp = Instantiate(PlanetsList[p], pos, Quaternion.identity);
            temp.GetComponent<Renderer>().sortingOrder = -1;
        }*/
    }
}
