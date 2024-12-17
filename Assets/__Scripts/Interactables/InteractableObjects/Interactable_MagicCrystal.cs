using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_MagicCrystal : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }

    [SerializeField] private string message = "Pick Up Gem";

    [SerializeField] GameObject crystal;


    bool hasKeyItem = false;
    private void Start()
    {
        PlayerData.instance.OnInventoryUpdate.AddListener(UpdateText);
    }
    public void Interact()
    {
        PlayerData.instance.AddGameobjectToInventory(crystal);
        crystal.SetActive(true);
        Destroy(gameObject);
    }
    private void UpdateText()
    {
        if (PlayerData.instance.ListContainsItemByName("Keyitem_PrisonCellKey"))
        {
            message = "Open Door";
            hasKeyItem = true;
        }
    }

    private void OnDestroy()
    {
        PlayerData.instance.OnInventoryUpdate.RemoveListener(UpdateText);
    }
}
