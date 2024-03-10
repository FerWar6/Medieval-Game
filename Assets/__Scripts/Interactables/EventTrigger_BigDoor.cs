using System.Collections;
using UnityEngine;

public class EventTrigger_BigDoor : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerEventWithDelay());
        }
    }

    private IEnumerator TriggerEventWithDelay()
    {
        yield return new WaitForSeconds(3f);

        if (anim != null)
        {
            anim.SetTrigger("CloseDoor");
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Animator component not found!");
        }
    }
}
