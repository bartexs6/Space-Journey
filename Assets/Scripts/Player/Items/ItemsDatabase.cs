using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDatabase
{
    private static List<Item> itemList = new List<Item>();
    private static JsonData itemData;
    private static GameObject prefab;

    static public void Initialize()
    {
        TextAsset load = Resources.Load<TextAsset>("GameData/itemsList");
        itemData = JsonMapper.ToObject(load.text);

        ConstructItemDatabase();
    }
    // Pozwala na wyszukiwanie itemu poprzez wpisanie id
    static public Item ItemById(int id)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].id == id)
            {
                Item newItem = new Item();
                newItem.id = itemList[i].id;
                newItem.name = itemList[i].name;
                newItem.description = itemList[i].description;
                newItem.isStackable = itemList[i].isStackable;
                newItem.itemIcon = itemList[i].itemIcon;

                return newItem;
            }
        }
        return null;
    }
    // Pozwala na wyszukiwanie itemu poprzez wpisanie nazwy
    static public Item ItemByName(string name)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].name == name)
            {
                Item newItem = new Item();
                newItem.id = itemList[i].id;
                newItem.name = itemList[i].name;
                newItem.description = itemList[i].description;
                newItem.isStackable = itemList[i].isStackable;
                newItem.itemIcon = itemList[i].itemIcon;

                return newItem;
            }
        }
        return null;
    }
    // Pobiera liste itemow z pliku JSON
    static void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            Item newItem = new Item();
            newItem.id = (int)itemData[i]["id"];
            newItem.name = itemData[i]["name"].ToString();
            newItem.description = itemData[i]["description"].ToString();
            newItem.isStackable = (bool)itemData[i]["isStackable"];
            
            itemList.Add(newItem);
        }
    }
}
