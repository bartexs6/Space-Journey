using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerShip))]


class Player : SpaceShip
{
    protected override int hp { get; set; }
    protected override float speed { get; set; }
    public override int maxSpeed { get; set; }
    public override int maxHp { get ; set ; }

    float tankVolume;

    public int attackDmg, experience, money;

    private bool isStayOnPlanet, isOnPanel = false, fuelNotification = false, isFilled = false;
    private Planet planet;

    private Rigidbody2D playerRigidbody;
    private PlayerMovement PlayerMovement;
    protected PlayerAttack PlayerAttack;
    private Vector2 oldPosition;
    void Start()
    {
        hp = 10;
        speed = 4.6f;
        maxSpeed = 80;
        attackDmg = 2;
        tankVolume = 1000;
        UI.updateHP(hp);

        PlayerMovement = gameObject.GetComponent<PlayerMovement>();
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        PlayerAttack = gameObject.GetComponent<PlayerAttack>();
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            UI.inventoryUI.SetActive(!UI.inventoryUI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerAttack.Skill_Rocket();
        }
        if (isStayOnPlanet)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isOnPanel == false)
                {
                    isOnPanel = true;
                    UI.setPlanetUI(planet);
                    UI.planetPanel.SetActive(isOnPanel);
                    Game.getPlanetCamera().SetActive(true);
                    Time.timeScale = 0f;
                }
                else
                {
                    exitPlanetPanel();
                }
            }
        }
        if (tankVolume <= 0)
        {
            PlayerMovement.accelerationx = 0;
            PlayerMovement.accelerationy = 0;
            if (fuelNotification == false)
            {
                UI.createNotification("Koniec paliwa! Zadzwoń po pomoc");
                fuelNotification = true;
            }
        }
        else
        {
            PlayerMovement.accelerationx = Input.GetAxisRaw("Horizontal");
            PlayerMovement.accelerationy = Input.GetAxisRaw("Vertical");
        }
        substractFuel(speed);
    }

    void FixedUpdate()
    {
        speed = Vector2.Distance(oldPosition, transform.position);
        oldPosition = transform.position;
    }

    public void exitPlanetPanel()
    {
        isOnPanel = false;
        UI.planetPanel.SetActive(isOnPanel);
        UI.hidePlayerUI();
        Game.getPlanetCamera().SetActive(false);
        Time.timeScale = 1f;
    }

    public void addFuel()
    {
        tankVolume += 1000;
        fuelNotification = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Planet":
                planet = other.gameObject.GetComponent<PlanetGameObject>().planet;
                Game.getPlanetCamera().transform.position = new Vector3(other.transform.position.x, other.transform.position.y, -2);
                isStayOnPlanet = true;
                break;
            case "Terminal":
                other.gameObject.GetComponent<QuestGiver>().GiveQuest();
                break;
            case "FuelStation":
                if (isFilled == false)
                {
                    addFuel();
                    isFilled = true;
                }
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Planet":
                planet = null;
                isStayOnPlanet = false;
                break;
            case "FuelStation":
                isFilled = false;
                break;
            default:
                break;
        }
    }

    public void explorePlanet()
    {
        planet.explorePlanet();
        exitPlanetPanel();
    }

    public void substractFuel(float fuelSpeed)
    {
        if (Input.GetKey(KeyCode.W))
        {
            tankVolume -= fuelSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            tankVolume -= fuelSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            tankVolume -= fuelSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            tankVolume -= fuelSpeed;
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
