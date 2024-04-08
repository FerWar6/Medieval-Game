using System.Collections;
using UnityEngine;

public class EventTrigger_BigDoor : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private EnemySpawner spawner;

    [SerializeField]
    private GameObject enemy1;
    [SerializeField]
    private GameObject enemy2;

    
    [SerializeField] float delay;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerEventWithDelay());
        }
    }

    private IEnumerator TriggerEventWithDelay()
    {
        yield return new WaitForSeconds(delay);

        if (anim != null)
        {
            anim.SetTrigger("CloseDoor");
            if(spawner != null)
            {
                spawner.startWaves = true;
                Destroy(enemy1);
                Destroy(enemy2);
            }
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Animator component not found!");
        }
    }
}
