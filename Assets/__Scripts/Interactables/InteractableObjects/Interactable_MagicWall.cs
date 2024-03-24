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
        if (PlayerData.instance.ListContainsItemByName("Keyitem_Jewel"))
        {
            wall.promptMessage = message;
        }
    }
    protected override void Interact()
    {

        if (PlayerData.instance.ListContainsItemByName("Keyitem_Jewel"))
        {
            PlayerData.instance.DestroyAllItemsInInventory();
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
