using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PrisionCellSword : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }

    [SerializeField] private string message = "Pick Up Sword";

    [SerializeField] AudioClip pickUpKey;

    public void Interact()
    {
        AudioManager.instance.SetAudioClip(pickUpKey, transform.position);
        GetComponent<Animator>().Play("PullOutSword");
    }
    public void SetInActive()
    {
        PlayerData.instance.AddGameobjectToInventory(gameObject);
        gameObject.SetActive(false);
    }
}
