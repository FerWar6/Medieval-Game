using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForEvent_PrisonExitDoor : MonoBehaviour
{
    private void Start()
    {
        PlayerData.instance.OnInventoryUpdate.AddListener(OpenDoor);
    }
    private void OpenDoor()
    {
        if (PlayerData.instance.ListContainsItemByName("Keyitem_PrisonSword"))
        {
            Animator anim = GetComponent<Animator>();
            anim.Play("DoorOpen");
            PlayerData.instance.OnInventoryUpdate.RemoveListener(OpenDoor);
        }
    }
    private void OnDestroy()
    {
        PlayerData.instance.OnInventoryUpdate.RemoveListener(OpenDoor);
    }
}
