using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance { get; private set; }

    [HideInInspector] public int playerHealth = 100;

    public List<GameObject> inventoryGameobjects;

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

    public void ModifyPlayerHealth(int number, bool add)
    {
        if (add)
        {
            playerHealth += number;
        }
        else
        {
            playerHealth -= number;
            if (playerHealth <= 0)
                OnPlayerDead();
        }
    }
    public void ResetPlayerHealth()
    {
        playerHealth = 100;
        Debug.Log("Reset Health");
    }
    private void OnPlayerDead()
    {
        SceneManager.LoadScene("DeathScreen");
        ResetPlayerHealth();
    }
}
