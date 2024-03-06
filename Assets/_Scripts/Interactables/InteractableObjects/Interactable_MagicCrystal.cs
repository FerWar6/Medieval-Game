using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_MagicCrystal : Interactable
{
    [SerializeField] GameObject crystal;
    [SerializeField] PlayerInventory playerInv;
    protected override void Interact()
    {
        playerInv.AddGameobjectToInventory(crystal);
        crystal.SetActive(true);
        Destroy(gameObject);
    }
}
