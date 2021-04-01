using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
class Player : SpaceShip
{
    protected override int hp { get; set; }
    protected override float speed { get; set; }
    protected override int maxSpeed { get; set; }

    int attackDmg, attackRate;

    protected Vector2 mousePos;
    protected Camera mainCamera;
    private Rigidbody2D playerRigidbody;
    private PlayerMovement PlayerMovement;
    protected PlayerAttack PlayerAttack;

   void Start()
    {
        hp = 10;
        speed = 4.6f;
        maxSpeed = 80;
        attackDmg = 2;
        UI.updateHP(hp);

        PlayerMovement = gameObject.GetComponent<PlayerMovement>();
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        PlayerAttack = gameObject.GetComponent<PlayerAttack>();
        mainCamera = Camera.main;
    }

    private void Update()
    {

        PlayerMovement.Movement();
        PlayerMovement.Aiming();

        if (Input.GetButtonDown("Fire1"))
        {
            PlayerAttack.PrimaryAttack();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            UI.mapUI.SetActive(!UI.mapUI.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            PlayerAttack.Skill_Rocket();
        }
    }

    public override int getHp()
    {
        return hp;
    }

    public override float getSpeed()
    {
        return speed;
    }

    public override int getMaxSpeed()
    {
        return maxSpeed;
    }
    public int getDmg()
    {
        return attackDmg;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        UI.updateHP(hp);
    }

    public Rigidbody2D getPlayerRigidbody()
    {
        return playerRigidbody;
    }
    public Vector2 getPosisition()
    {
        return playerRigidbody.transform.position;
    }
}
