using System.Collections;
using UnityEngine;

public class SecretBook : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    private float clickTime;
    private float clickThreshold = 0.3f;
    private float cooldownTime = 5.0f;
    private bool canPlayParticle = true;

    void Update()
    {
        if (canPlayParticle && Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverCollider())
            {
                if (Time.time - clickTime < clickThreshold)
                {
                    particle.Play();
                    StartCoroutine(Cooldown());
                }
                else
                {
                    clickTime = Time.time;
                }
            }
        }
    }

    private IEnumerator Cooldown()
    {
        canPlayParticle = false;
        yield return new WaitForSeconds(cooldownTime);
        canPlayParticle = true;
    }

    private bool IsMouseOverCollider()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider == GetComponent<Collider>();
        }

        return false;
    }
}
