using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* UI */
static public class UI
{
    // Canvas
    static public GameObject canvas;
    // Tekt wyświtlający informacje
    static public GameObject infoText;
    // Panel planety
    static public GameObject planetPanel;
        // ELEMENTY PANELU PLANETY
        static public TMP_Text planetPanelName;
        static public TMP_Text planetPanelType;
        static public TMP_Text planetPanelFaction;
        static public TMP_Text planetPanelInfo;
    // ------------------------
    // Panel statku
    static public GameObject shipPanel;
    // ELEMENTY PANELU STATKU


    // ------------------------
    // HP
    static public TMPro.TMP_Text hpUI;
    // UI mapy
    static public GameObject mapUI;
    // Panel błędów
    static public GameObject errorPanel;
    // Notyfikacja
    public static GameObject notification;
    // Lista notyfikacji
    public static List<GameObject> notifications = new List<GameObject>();
    // Ekwipunek
    public static GameObject inventoryUI;
    public static GameObject itemUIPrefab;

    // Zainicjalizuj
    public static bool Initialize()
    {
        // Spróbuj
        try
        {
            // Znaleźć obiekty
            canvas = GameObject.Find("Canvas");
            planetPanel = canvas.transform.Find("PlanetPanel").gameObject;
            mapUI = canvas.transform.Find("Map").gameObject;
            hpUI = canvas.transform.Find("PlayerUI/HP").GetComponent<TMP_Text>();
            errorPanel = canvas.transform.Find("ErrorPanel").gameObject;
            inventoryUI = canvas.transform.Find("InventoryUI").gameObject;
            shipPanel = canvas.transform.Find("ShipPanel").gameObject;

            planetPanelName = planetPanel.transform.Find("PlanetName/Text").GetComponent<TMP_Text>();
            planetPanelType = planetPanel.transform.Find("PlanetType/Text").GetComponent<TMP_Text>();
            planetPanelFaction = planetPanel.transform.Find("PlanetFactionText/Text").GetComponent<TMP_Text>();
            planetPanelInfo = planetPanel.transform.Find("PlanetBackground/LeftBackground/Text").GetComponent<TMP_Text>();


            return true;
        }
        catch (Exception e) // W przeciwnym razie
        {
            // Wyśiwetl błąd
            Debug.LogError("Couldn't find gameobject in UI.cs");
            Debug.LogError(e);
            return false;
        }
    }

   public static void addItemUI(Item item)
    {
        GameObject temp = GameObject.Instantiate(itemUIPrefab, inventoryUI.transform.Find("ItemsList"));
        temp.GetComponent<TMP_Text>().text = item.name + "\n" + item.description;
    }

    // Wyświetl błąd
    public static void createErrorUI(string message)
    {
        errorPanel.GetComponentInChildren<TMP_Text>().text = message;
        errorPanel.SetActive(true);
    }
    
    // Ustaw panel statku
    public static void setShipUI()
    {
        shipPanel.transform.Find("ShipType/Text").GetComponent<TMP_Text>().text = Game.getPlayer().GetComponent<PlayerShip>().shipType.ToString().Substring(0, 1);
        shipPanel.transform.Find("ShipName/Text").GetComponent<TMP_Text>().text = Game.getPlayer().GetComponent<PlayerShip>().shipName;

        shipPanel.transform.Find("Info/Health/Slider/Text").GetComponent<TMP_Text>().text = "MAX: " + Game.getPlayer().maxHp + " HP";
        shipPanel.transform.Find("Info/Health/Slider").GetComponent<Slider>().maxValue = Game.getPlayer().maxHp;
        shipPanel.transform.Find("Info/Health/Slider").GetComponent<Slider>().value = Game.getPlayer().getHp();

        shipPanel.transform.Find("Info/Ammunition/Slider/Text").GetComponent<TMP_Text>().text = "MAX: " + Game.getPlayer().attackDmg + " DMG";
        shipPanel.transform.Find("Info/Ammunition/Slider").GetComponent<Slider>().maxValue = Game.getPlayer().attackDmg;
        shipPanel.transform.Find("Info/Ammunition/Slider").GetComponent<Slider>().value = Game.getPlayer().attackDmg;

        shipPanel.transform.Find("Info/Speed/Slider/Text").GetComponent<TMP_Text>().text = "MAX: " + Game.getPlayer().maxSpeed + "K KM/H";
        shipPanel.transform.Find("Info/Speed/Slider").GetComponent<Slider>().maxValue = Game.getPlayer().maxSpeed;
        shipPanel.transform.Find("Info/Speed/Slider").GetComponent<Slider>().value = Game.getPlayer().getSpeed();

        shipPanel.transform.Find("Info/Fuel/Slider/Text").GetComponent<TMP_Text>().text = "MAX: " + Game.getPlayer().maxVolume + "L";
        shipPanel.transform.Find("Info/Fuel/Slider").GetComponent<Slider>().maxValue = Game.getPlayer().maxVolume;
        shipPanel.transform.Find("Info/Fuel/Slider").GetComponent<Slider>().value = Game.getPlayer().tankVolume;
    }

    // Ustaw panel planety
    public static void setPlanetUI(Planet planet)
    {
        planetPanelName.text = planet.name;
        planetPanelType.text = planet.planetType.ToString().Substring(0, 1);
        planetPanelFaction.text = "FACTION:" + planet.planetFaction.ToString().ToUpper();

        planetPanelInfo.text = $"NAME: {planet.name}\n\n" +
            $"AGE: {planet.age}mln years\n" +
            "SURFACE: costam\n" +
            $"AVERAGE TEMPERATURE:\n {planet.temperature}C\n\n" +
            "RESOURCES:\n" +
            $"- {planet.resources[0]}\n" +
            $"- {planet.resources[1]}\n\n" +
            "COLONIZED: no\n\n" +
            $"TIME TO EXPLORE:\n {planet.experience / 2} seconds\n" +
            $"EXPERIENCE: {planet.experience}";

        if (planet.planetStatus == PlanetStatus.Inhabited)
        {
            planetPanel.transform.Find("PlanetBackground/RightBackground").gameObject.SetActive(false);

            GameObject cityBackground = planetPanel.transform.Find("PlanetBackground/CityBackground").gameObject;
            cityBackground.SetActive(true);

            cityBackground.transform.Find("Reputation/Slider/Text").GetComponent<TMP_Text>().text = $"{planet.city.playerReputation}/100";
            cityBackground.transform.Find("Reputation/Slider").GetComponent<Slider>().value = planet.city.playerReputation;

            cityBackground.transform.Find("Health/Slider/Text").GetComponent<TMP_Text>().text = $"{planet.city.health}/100";
            cityBackground.transform.Find("Health/Slider").GetComponent<Slider>().value = planet.city.health;

            cityBackground.transform.Find("Temperature/Slider/Text").GetComponent<TMP_Text>().text = $"{planet.temperature}C";
            cityBackground.transform.Find("Temperature/Slider").GetComponent<Slider>().value = planet.temperature;
            cityBackground.transform.Find("Temperature/Slider/Fill Area/Fill").GetComponent<Image>().color = (planet.temperature > 50) ? Color.red : (planet.temperature < -30) ? Color.blue : Color.green;

            cityBackground.transform.Find("Population/Slider/Text").GetComponent<TMP_Text>().text = $"{planet.city.population}/10000mln";
            cityBackground.transform.Find("Population/Slider").GetComponent<Slider>().value = planet.city.population;
        }
        else
        {
            planetPanel.transform.Find("PlanetBackground/RightBackground").gameObject.SetActive(true);
            planetPanel.transform.Find("PlanetBackground/CityBackground").gameObject.SetActive(false);
        }

        if (planet.isExplored)
        {
            planetPanel.transform.Find("PlanetBackground/RightBackground/Blocked").gameObject.SetActive(false);
            planetPanel.transform.Find("PlanetBackground/DownBackground/ExploreButton").GetComponent<Button>().interactable = false;
            planetPanel.transform.Find("PlanetBackground/DownBackground/ExploreButton/Text").GetComponent<TMP_Text>().text = "EXPLORED";

        }
        else
        {
            planetPanel.transform.Find("PlanetBackground/RightBackground/Blocked").gameObject.SetActive(true);
            planetPanel.transform.Find("PlanetBackground/DownBackground/ExploreButton").GetComponent<Button>().interactable = true;
            planetPanel.transform.Find("PlanetBackground/DownBackground/ExploreButton/Text").GetComponent<TMP_Text>().text = "EXPLORE";
        }

        hidePlayerUI();
    }

    public static void createNotification(string message, int seconds = 4)
    {
        int childs = canvas.transform.Find("PlayerUI/Notifications").childCount;
        childs = (childs != 0) ? childs - 1 : 0;

        Vector2 currentNotifications = new Vector2(2085, 1040);
        int id = 0;

        if (canvas.transform.Find("PlayerUI/Notifications").childCount == 0)
        {
            currentNotifications = new Vector2(2085, 1040);
            id = 0;
        }
        else
        {
            for (int i = 0; i <= notifications.Count; i++)
            {
                if (notifications[0] == null)
                {
                    id = i;
                    currentNotifications = new Vector2(2085, 1040);
                    break;
                }
                else 
                {
                    id = i;
                    currentNotifications = new Vector2(canvas.transform.Find("PlayerUI/Notifications").GetChild(childs).position.x + 800, canvas.transform.Find("PlayerUI/Notifications").GetChild(childs).position.y - (60));
                }
            }
        }

        GameObject notificationTemp = GameObject.Instantiate(notification, currentNotifications, Quaternion.identity, canvas.transform.Find("PlayerUI/Notifications"));
        notificationTemp.GetComponentInChildren<TMP_Text>().text = message;

        notifications.Insert(id, notificationTemp);

        Tween.notificationAnimation(notificationTemp, seconds, id);
    }

    public static void hidePlayerUI()
    {
        canvas.transform.Find("PlayerUI").gameObject.SetActive(!canvas.transform.Find("PlayerUI").gameObject.activeSelf);
    }

    // Zaktualizuj tekst HP
    public static void updateHP(int hp)
    {
        hpUI.text = "HP: " + hp;
    }
}
