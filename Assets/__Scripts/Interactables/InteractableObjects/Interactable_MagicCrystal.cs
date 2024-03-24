using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_MagicCrystal : Interactable
{
    [SerializeField] GameObject crystal;
    protected override void Interact()
    {
        PlayerData.instance.AddGameobjectToInventory(crystal);
        crystal.SetActive(true);
        Destroy(gameObject);
    }
}
