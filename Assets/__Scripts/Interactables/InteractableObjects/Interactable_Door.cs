using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Door : Interactable
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    protected override void Interact()
    {
        anim.SetTrigger("Opened");
        Debug.Log("interacted");
    }
}
