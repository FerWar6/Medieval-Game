using System.Collections;
using UnityEngine;

public class Spear_Trap_Manager : MonoBehaviour
{
    private Animator trapAnimator;

    private void Start()
    {
        trapAnimator = GetComponent<Animator>();

        if (trapAnimator == null)
        {
            Debug.LogError("Animator component not found on Spear Trap Manager GameObject.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trapAnimator.SetTrigger("ExtendTrap");
            StartCoroutine(ReturnToIdleAfterDelay(5f));
        }
    }

    private IEnumerator ReturnToIdleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        trapAnimator.SetTrigger("ReturnSpear");
        trapAnimator.ResetTrigger("ExtendTrap"); 
    }
}
