using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Pocisk */
public class BulletScript : MonoBehaviour, IPooledObject
{
    // Zmienna przechowująca particle
    public ParticleSystem HitEffect;

    int dmg;

    // Strzelający
    private string sender;

    // Rigidbody
    Rigidbody2D rb;

    // Podczas stworzenia obiektu przypisz Rigidbody do zmiennej rb
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Podczas stworzenia obiektu
    public void OnObjectSpawn()
    {
        // Dodaj siłę pocisku
        rb.AddForce(transform.up * 8, ForceMode2D.Impulse);
        // Uruchom funkcję Disable po 5 sekundach
        Invoke("Disable", 5);
    }

    // Podczas wyłączania pocisku
    void Disable()
    {
        // Anuluj wywoływanie funkcji co 5 sekund
        CancelInvoke();
        // Prędkośc pocisku do zera
        rb.velocity = Vector2.zero;
        // Wyłącz obiekt
        gameObject.SetActive(false);
    }
    // --------------------------- //

    // DO ZMIANY

    public void GetSender(string s, int dmg)
    {
        sender = s;
        this.dmg = dmg;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (sender)
        {
            case "Enemy":
             
                if(collision.tag == "Player")
                {
                    if (collision.GetComponent<Player>() != null)
                    {
                        collision.transform.GetComponent<Player>().TakeDamage(dmg);
                        //ParticleSystem effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                        Disable();
                    }
                    else
                    {
                        //ParticleSystem effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                        Disable();
                    }
                }

                break;

            case "Player":

                if (collision.GetComponent<Enemy>() != null)
                {
                    collision.transform.GetComponent<Enemy>().TakeDamage(dmg);
                    //ParticleSystem effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
                    Disable();
                }

                break;

            
        }
        
    }
    
    
}
