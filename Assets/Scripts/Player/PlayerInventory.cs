using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerInventory : MonoBehaviour
{
    private void Start()
    {
        addItem(ItemsDatabase.ItemById(0), 15);
        addItem(ItemsDatabase.ItemById(0), 15);
        addItem(ItemsDatabase.ItemById(0), 15);
        Debug.Log(playerInventory.Count);
        for (int i = 0; i < playerInventory.Count; i++)
        {
            Debug.Log(playerInventory[i].Amount + " " + playerInventory[i].name);
        }
    }
    List<Item> playerInventory = new List<Item>();

    public void addItem(Item item, short amount = 1)
    {
        if (playerInventory.Count <= Player.playerInventorySize)
        {
            if (item.isStackable == true)
            {
                if (playerInventory.Count == 0)
                {
                    item.Amount = amount;
                    playerInventory.Add(item);
                    UI.addItemUI(item);
                }
                else
                {
                    List<Item> temp = playerInventory.FindAll((i) => i.id == item.id);
                    int iteration = 0;
                    foreach (var i in temp)
                    {
                        iteration++;
                        if (i.Amount == 1000)
                        {
                            if (iteration < temp.Count)
                            {
                                continue;
                            }
                            else if (iteration == temp.Count)
                            {
                                item.Amount = amount;
                                Debug.Log(item.Amount);
                                playerInventory.Add(item);
                                UI.addItemUI(item);
                            }
                        }
                        else
                        {
                            if (i.Amount + amount > 1000)
                            {
                                int tempAmount = i.Amount + amount - 1000;
                                i.Amount = 1000;
                                item.Amount = (short)tempAmount;
                                playerInventory.Add(item);
                                UI.addItemUI(item);
                            }
                            else
                            {
                                i.Amount += amount;
                            }
                        }
                    }
                    if (!playerInventory.Exists((i) => i.id == item.id))
                    {
                        item.Amount = amount;
                        playerInventory.Add(item);
                        UI.addItemUI(item);
                    }
                }
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    item.Amount = 1;
                    playerInventory.Add(item);
                    UI.addItemUI(item);
                }
            }
        }
        else
        {
            Debug.Log("essa");
        }
    }

}
