using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance { get; private set; }

    public Transform playerPos;
    public int playerHealth = 100;

    public List<GameObject> inventoryGameobjects;

    public UnityEvent OnHealthChanged = new UnityEvent();

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
        OnHealthChanged.Invoke();
    }
    public void ResetPlayerHealth()
    {
        playerHealth = 100;
        Debug.Log("Reset Health");
    }
    private void OnPlayerDead()
    {
        SettingsManager.instance.deathScreenOn = true;
        SceneManager.LoadScene("DeathScreen");
        ResetPlayerHealth();
    }
}
