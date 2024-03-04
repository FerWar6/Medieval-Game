using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_MagicCrystal : Interactable
{
    protected override void Interact()
    {
        Debug.Log("interacted");
        Destroy(gameObject);
    }
}
