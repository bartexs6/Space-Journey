using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Klasa wymaga innej klasy - EnemyAttack
[RequireComponent(typeof(EnemyAttack))]

/* Przeciwnik */

class Enemy : SpaceShip
{
    protected override int hp { get ; set ; }
    protected override float speed { get ; set ; }
    public override int maxSpeed { get ; set ; }
    public override int maxHp { get ; set ; }

    // Rigidbody przeciwnika
    private Rigidbody2D enemyRigidbody;

    int attackDmg, attackRate;

    // Obiekt wyświetlania otrzymanego obrażenia
    private GameObject dmgDisplay;

    // Podczas włączenia przeciwnika
    void Awake()
    {
        enemyRigidbody = gameObject.GetComponent<Rigidbody2D>();

        // Czyta gameobject z folderu /Resources/Text/DmgDisplay
        dmgDisplay = Resources.Load<GameObject>("Text/DmgDisplay");

        hp = 1;
        attackDmg = 1;
    }

    // --- Każda funkcja pozwala na pobranie danej wartości ---
    public override int getHp()
    {
        return hp;
    }

    public override float getSpeed()
    {
        return speed;
    }

    public int getDmg()
    {
        return attackDmg;
    }
    // --- ---

    // Funkcja otrzymywania obrażeń; dmg to liczba otrzymanych obrażeń
    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        // Tworzenie tekstu ukazującego otrzymane obrażenia
        GameObject txt = Instantiate(dmgDisplay, transform.position, Quaternion.identity);
        txt.GetComponentInChildren<TextMeshProUGUI>().text = dmg.ToString();

        // Jeżeli ma mniej niż 0 hp niszczy obiekt za pomocą funkcji DestroyFighter w EnemyControler
        if (hp <= 0)
        {
            GameEvents.current.EnemyCountTrigger();

            Game.getEnemyControler().DestroyFighter(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject);
        }
    }

}
