using UnityEngine;

public class Interactable_Gate : Interactable
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    protected override void Interact()
    {
        anim.SetTrigger("CloseGate");
        Debug.Log("interacted");

    }
}
