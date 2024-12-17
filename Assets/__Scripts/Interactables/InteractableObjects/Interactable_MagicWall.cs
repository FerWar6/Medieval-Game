using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_MagicWall : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return hasKeyItem ? originalMessage : null; } }

    private Animator anim;

    private string originalMessage;
    private bool hasKeyItem = false;

    private void Start()
    {
        originalMessage = "Open Wall";
        anim = GetComponent<Animator>();
        PlayerData.instance.OnInventoryUpdate.AddListener(UpdateKeyItemStatus);
    }

    private void UpdateKeyItemStatus()
    {
        hasKeyItem = PlayerData.instance.ListContainsItemByName("Keyitem_Jewel");
    }

    public void Interact()
    {
        if (hasKeyItem)
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

    private void OnDestroy()
    {
        PlayerData.instance.OnInventoryUpdate.RemoveListener(UpdateKeyItemStatus);
    }
}
