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
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerEventWithDelay());
        }
    }

    private IEnumerator TriggerEventWithDelay()
    {
        yield return new WaitForSeconds(0.1f);

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
