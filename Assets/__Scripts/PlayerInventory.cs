using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

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
    public bool ListContainsItemByName(string name)
    {
        foreach (GameObject item in inventoryGameobjects)
        {
            if (item.name == name)
            {
                return true;
            }
        }

        return false;
    }
}
