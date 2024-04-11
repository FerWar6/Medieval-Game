using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Door : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }
    [SerializeField] private string message;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Interact()
    {
        anim.SetTrigger("Opened");
    }
}
