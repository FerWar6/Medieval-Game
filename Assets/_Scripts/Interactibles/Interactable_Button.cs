using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Button : Interactable
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    protected override void Interact()
    {
        anim.SetBool("pressed", true);
        Debug.Log("interacted");
    }
}
