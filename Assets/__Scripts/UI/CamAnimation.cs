using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimation : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void PlayBloodEffect()
    {
        anim.ResetTrigger("Reset");
        StartCoroutine(WaitForAnimation());
    }

    private System.Collections.IEnumerator WaitForAnimation()
    {
        anim.SetTrigger("BloodEffect");
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("Reset");
        anim.ResetTrigger("BloodEffect");
    }
}
