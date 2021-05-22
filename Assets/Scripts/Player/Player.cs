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

    public float tankVolume;
    public float maxVolume;
    public static int playerInventorySize;

    public int attackDmg, experience, money;

    private bool fuelNotification = false, isFilled = false, isOverFilled = false;
    private Planet planet;

    private Rigidbody2D playerRigidbody;
    private PlayerMovement PlayerMovement;
    protected PlayerAttack PlayerAttack;
    private Vector2 oldPosition;

    public bool readyToInteract = false;
    private IInteractible interactible;
    void Start()
    {
        hp = 10;
        maxHp = 10;
        speed = 4.6f;
        maxSpeed = 80;
        attackDmg = 2;
        tankVolume = 1000;
        maxVolume = 2000;
        playerInventorySize = 20;
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            UI.setShipUI();
            UI.shipPanel.SetActive(!UI.shipPanel.activeSelf);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Game.SaveGame();
            }
        }

        if(readyToInteract)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                interactible?.Interact();
            }
        }

        CheckFuelStatus();

        substractFuel(speed);
    }

    void FixedUpdate()
    {
        speed = Vector2.Distance(oldPosition, transform.position);
        oldPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<IInteractible>() != null)
        {
            interactible = other.GetComponent<IInteractible>();
            readyToInteract = true;
        }              
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<IInteractible>() != null)
        {
            readyToInteract = false;
        }
    }

    #region Funkcje Planety
    public void explorePlanet()
    {
        planet.toExplored = TickTimeManager.GetTick() + (int)((planet.experience/2)/TickTimeManager.TICK_MAX);
        TickJobs.Create(planet.explorePlanet, (planet.experience / 2));
        exitPlanetPanel();
    }

    public void PlanetSetUp(Planet newPlanet, Vector3 planetPosition)
    {
        planet = newPlanet;
        Game.getPlanetCamera().transform.position = new Vector3(planetPosition.x, planetPosition.y, -2);
        UI.setPlanetUI(planet);
        UI.planetPanel.SetActive(true);
        Game.getPlanetCamera().SetActive(true);
        Time.timeScale = 0f;
    }
    public void exitPlanetPanel()
    {
        planet = null;
        UI.planetPanel.SetActive(false);
        UI.hidePlayerUI();
        Game.getPlanetCamera().SetActive(false);
        Time.timeScale = 1f;
    }

    #endregion

    #region Funkcje Stacji Paliw
    public void substractFuel(float fuelSpeed)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        tankVolume -= ((horizontal * horizontal) + (vertical * vertical)) * fuelSpeed;       
    }

    private void CheckFuelStatus()
    {
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
    }

    public bool TankStatus()
    {
        return isOverFilled ? false : true;
    }

    public void UpdateTankStatus()
    {
        if (tankVolume >= maxVolume)
        {
            tankVolume = maxVolume;
            isOverFilled = true;
            if (fuelNotification == false)
            {
                UI.createNotification("Bak pełny! Nie zmieścisz więcej paliwa");
                fuelNotification = true;
            }
        }
        else
        {
            isOverFilled = false;
        }
    }

    #endregion

    #region Ogólne Funkcje typu Get

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

    #endregion
}
