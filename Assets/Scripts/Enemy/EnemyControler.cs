using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Kontrolowanie wszystkich przeciwników */
public class EnemyControler : MonoBehaviour
{
    // Zmienna przechowująca gracza
    private Transform player;
    // Lista zawierająca wszystkich przeciwników
    private List<Rigidbody2D> EnemyList = new List<Rigidbody2D>();
    // Zmienna sterująca pętlą while
    int i;

    void Start()
    {
        // Zapisuje do zmiennej player, playera
        player = Game.getPlayer().transform;

        // Każdy obiekt z tagiem Enemy zostaje zapisany do listy EnemyList
        foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyList.Add(item.GetComponent<Rigidbody2D>());
        }
    }

    // Usuń przeciwnika z listy
    public void DestroyFighter(Rigidbody2D Fighter)
    {
        EnemyList.Remove(Fighter);
    }

    void Update()
    {
        // Jeżeli gracz żyje
        if (Game.getPlayer().enabled)
        {
            // Dla każdego obiektu z listy EnemyList
            while (i < EnemyList.Count)
            {
                // Pozycja patrzenia oraz jego kąt
                Vector2 lookDir = new Vector2(player.position.x - EnemyList[i].transform.position.x, player.position.y - EnemyList[i].transform.position.y);
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                EnemyList[i].rotation = angle;

                // Jeżeli dystans do gracza jest mniejszy niż 30 jednostek
                if (Vector2.Distance(EnemyList[i].transform.position, player.position) < 30)
                {
                    // Jeżeli dystans do gracza jest większy niż 8 jednostek
                    if (Vector2.Distance(EnemyList[i].transform.position, player.position) > 8)
                    {
                        // Dodaj prędkość w stronę gracza
                        EnemyList[i].AddForce(EnemyList[i].transform.up * 0.5f, ForceMode2D.Force);
                    }
                    else
                    {
                        // Zatrzymaj statek
                        EnemyList[i].velocity = EnemyList[i].velocity.normalized * 0.4f;
                        
                        // Strzel w stronę gracza za pomocą funkcji Shooting w EnemyAttack
                        EnemyList[i].GetComponent<EnemyAttack>().Shooting();
                    }
                }
                else
                {
                    // Kieruj się w stronę gracza
                    transform.position = Vector2.MoveTowards(EnemyList[i].transform.position, player.position, 100 * Time.deltaTime);
                }

                i++;
            }

            // Jeżeli liczba kontrolna pętli jest równa liczbie obiektów to wyzeruj liczbę kontrolną i rozpocznij pętle od nowa
            if(i >= EnemyList.Count - 1)
            {
                i = 0;
            }

            /* Ta pętla działa w ten sam sposób jak pętla for przedstawiona poniżej, lecz z lepsza optymalizacją
            for (int i = 0; i < EnemyList.Count; i++)
            {

            }
             */
        }
    }
}
