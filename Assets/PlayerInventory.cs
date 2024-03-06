using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> inventoryGameobjects;

    void Start()
    {
        inventoryGameobjects = new List<GameObject>();
    }
    public void AddGameobjectToInventory(GameObject gameobject)
    {
        inventoryGameobjects.Add(gameobject);
    }
    public void DestroyAllItemsInInventory()
    {
        foreach (GameObject item in inventoryGameobjects)
        {
            Destroy(item);
        }

        inventoryGameobjects.Clear();
    }
}
