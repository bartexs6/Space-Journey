using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    GameObject planetPanel;
    GameObject presText;
    bool isStayOnPlanet = false;
    bool isSpacePressed = false;
    int Sricks;

    private void Start()
    {
        Sricks += 6;
    }

    private void Update()
    {
        if (isStayOnPlanet)
        {
            if (Input.GetKeyDown(KeyCode.Space) && planetPanel.activeSelf == false)
            {
                Sricks = TickTimeManager.GetTick() + 6;
                isSpacePressed = true;
                presText.SetActive(false);
                planetPanel.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && planetPanel.activeSelf == true)
            {
                isSpacePressed = false;
                presText.SetActive(true);
                planetPanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        if (isSpacePressed)
        {
            if (TickTimeManager.GetTick() >= Sricks)
            {
                isSpacePressed = false;
                Sricks = TickTimeManager.GetTick() + 6;
                Time.timeScale = 0f;
            }
        }
        
    }

    private void Awake()
    {
        presText = UI.pressText;
        planetPanel = UI.planetPanel;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            presText.SetActive(true);
            isStayOnPlanet = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            presText.SetActive(false);
            isStayOnPlanet = false;
        }
    }


}
