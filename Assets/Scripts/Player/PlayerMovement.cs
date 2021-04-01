using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DO ZMIANY - BEZ KOMENTARZY
class PlayerMovement : MonoBehaviour
{
    Player Player;
    float speed, maxSpeed;
    Rigidbody2D playerRigidbody;
    Vector2 mousePos;

    bool chunkLoaded = false;
    Vector2 savedPositon;

    private void Start()
    {
        Player = Game.getPlayer();
        speed = Player.getSpeed();
        maxSpeed = Player.getMaxSpeed();
        playerRigidbody = Player.getPlayerRigidbody();
    }
    public void Movement()
    {
        float accelerationx = Input.GetAxisRaw("Horizontal");
        float accelerationy = Input.GetAxisRaw("Vertical");

        playerRigidbody.AddForce(new Vector2(accelerationx * speed, accelerationy * speed), ForceMode2D.Force);

        if (playerRigidbody.velocity.magnitude > maxSpeed)
        {
            playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxSpeed;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Tymczasowe ladowanie chunkow
        
        

        if (System.Math.Abs(playerRigidbody.position.x + savedPositon.x) >= World.chunkSize / 2 || System.Math.Abs(playerRigidbody.position.y + savedPositon.y) >= World.chunkSize / 2)
        {
            chunkLoaded = false;
            savedPositon = playerRigidbody.position;
        }

        if(System.Math.Abs((int)playerRigidbody.position.x) % World.chunkSize == 0 && !chunkLoaded)
        {
            loadPlayerChunk();
        }
        if(System.Math.Abs((int)playerRigidbody.position.y) % World.chunkSize == 0 && !chunkLoaded)
        {
            loadPlayerChunk();
        }

        // ---
    }

    void loadPlayerChunk()
    {
        // Załaduj chunki dookoła gracza

        Vector2 mid = playerRigidbody.position;

        if (playerRigidbody.position.x >= 0)
        {
            mid = new Vector2(Mathf.Round(((int)playerRigidbody.position.x + World.chunkSize) / 1000) * 1000, Mathf.Round((int)playerRigidbody.position.y / 1000) * 1000);
        }
        if (playerRigidbody.position.y >= 0)
        {
            mid = new Vector2(Mathf.Round((int)playerRigidbody.position.x / 1000) * 1000, Mathf.Round((int)playerRigidbody.position.y / 1000) * 1000);
        }
        if (playerRigidbody.position.x < 0)
        {
            mid = new Vector2(Mathf.Round(((int)playerRigidbody.position.x - World.chunkSize) / 1000) * 1000, Mathf.Round((int)playerRigidbody.position.y / 1000) * 1000);
        }
        if (playerRigidbody.position.y < 0)
        {
            mid = new Vector2(Mathf.Round((int)playerRigidbody.position.x / 1000) * 1000, Mathf.Round(((int)playerRigidbody.position.y - World.chunkSize) / 1000) * 1000);
        }

        int range = World.chunkSize;

        for (int x = (int)mid.x - range; x <= mid.x + range; x += range)
        {
            for (int y = (int)mid.y - range; y <= mid.y + range; y += range)
            {
                Vector2 targetChunk = new Vector2(x, y);
                World.LoadChunk(targetChunk);
            }
        }

        savedPositon = new Vector2((int)playerRigidbody.position.x, (int)playerRigidbody.position.y);
        chunkLoaded = true;
    }

    public void Aiming()
    {
        Vector2 lookDir = mousePos - playerRigidbody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        playerRigidbody.rotation = angle;
    }

    
}
