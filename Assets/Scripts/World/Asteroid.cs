using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    public ResourcesType resources;

    private void OnEnable()
    {
        short randomAsteroid = (short)Random.Range(1, 100);
        if(randomAsteroid <= 20)
        {
            GetComponent<SpriteRenderer>().sprite = Game.getGameManager().Asteroids[2];
            resources = ResourcesType.Gold;
        }
        else
        {
            Debug.Log(randomAsteroid);
            GetComponent<SpriteRenderer>().sprite = (randomAsteroid <= 40) ? Game.getGameManager().Asteroids[1] : Game.getGameManager().Asteroids[0];
            resources = ResourcesType.Iron;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage()
    {
        short amount = (short)Random.Range(10, 200);
        Game.getPlayerInventory().addItem(ItemsDatabase.ItemByName(resources.ToString()), amount);

        Destroy(this.gameObject);

        GameObject temp = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/item"), transform.position, Quaternion.identity);
        temp.transform.DOMove(Game.getPlayer().getPosisition(), 1f).OnComplete(() => { 
            var dmgDisplay = Resources.Load<GameObject>("Text/DmgDisplay");

            GameObject txt = Instantiate(dmgDisplay, temp.transform.position, Quaternion.identity);
            txt.GetComponentInChildren<TMP_Text>().text = "+" + amount;

            GameObject.Destroy(temp); });
    }
}