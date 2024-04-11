using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PrisonCellKey : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }

    [SerializeField] private string message = "Pick Up Key";

    [SerializeField] AudioClip pickUpKey;

    public void Interact()
    {
        PlayerData.instance.AddGameobjectToInventory(gameObject);
        AudioManager.instance.SetAudioClip(pickUpKey, transform.position);
        Destroy(gameObject);
    }
}
