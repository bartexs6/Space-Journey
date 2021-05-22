using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlanetGameObject : MonoBehaviour, IInteractible
{
    public Planet planet;

    void OnBecameInvisible()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    void OnBecameVisible()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void Interact()
    {
        Player p = Game.getPlayer();
        p.PlanetSetUp(planet, gameObject.transform.position);
    }
}
