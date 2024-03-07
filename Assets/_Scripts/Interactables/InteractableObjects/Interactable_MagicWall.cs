using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_MagicWall : Interactable
{
    Interactable_MagicWall wall;
    private Animator anim;

    string message = null;
    private void Start()
    {
        wall = GetComponent<Interactable_MagicWall>();
        message = wall.promptMessage;
        wall.promptMessage = null;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (PlayerInventory.instance.ListContainsItemByName("Keyitem_Jewel"))
        {
            wall.promptMessage = message;
        }
    }
    protected override void Interact()
    {

        if (PlayerInventory.instance.ListContainsItemByName("Keyitem_Jewel"))
        {
            PlayerInventory.instance.DestroyAllItemsInInventory();
            anim.SetTrigger("OpenDoor");
            DestroyInteractable();
        }
    }
    private void DestroyInteractable()
    {
        Destroy(GetComponent<BoxCollider>());

        Destroy(this);
    }
}
