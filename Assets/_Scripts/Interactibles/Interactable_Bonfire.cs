using UnityEngine;

public class Interactable_Bonfire : Interactable
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    protected override void Interact()
    {
        anim.SetBool("onFire", true);
        Debug.Log("interacted");
    }
}
