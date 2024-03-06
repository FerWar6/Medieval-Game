using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_MagicWall : Interactable
{
    [SerializeField]  Interactable_MagicWall wall;
    [SerializeField] PlayerInventory inv;
    private Animator anim;

    string message = null;
    private void Start()
    {
        message = wall.promptMessage;
        wall.promptMessage = null;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(inv.inventoryGameobjects.Count == 1)
        {
            wall.promptMessage = message;
        }
    }
    protected override void Interact()
    {
        if (inv.inventoryGameobjects.Count == 1)
        {
            inv.DestroyAllItemsInInventory();
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
