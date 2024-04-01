using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] LayerMask playerLayerMask;

    private Rigidbody rb;

    private bool lockProjectile = false;
    private bool ableToSetLastVariables = true;
    
    private Quaternion lastRotation;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((groundLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            LockProjectile();
            StartCoroutine(DelayAndDestroy(0.5f));
        }
        if ((playerLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            LockProjectile();
            PlayerData.instance.ModifyPlayerHealth(10, false);
            Camera.main.GetComponent<CamAnimation>().PlayBloodEffect();
            PlayParticleAndDestroy();
        }
    }

    private IEnumerator DelayAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    private void PlayParticleAndDestroy()
    {
        // Play a particle effect
        Destroy(gameObject);
    }
    private void Update()
    {
        if (!lockProjectile)
        {
            if (rb.velocity != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(rb.velocity);
                transform.rotation = rotation;
                if (ableToSetLastVariables)
                {
                    StartCoroutine(SetLastVariables(rotation, transform));
                }
            }
        }
    }
    private IEnumerator SetLastVariables(Quaternion rotation, Transform transform)
    {
        ableToSetLastVariables = false;
        lastRotation = rotation;
        yield return new WaitForSeconds(0.02f);
        ableToSetLastVariables = true;
    }
    private void LockProjectile()
    {
        lockProjectile = true;
        transform.rotation = lastRotation;
        GetComponent<SphereCollider>().enabled = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
    }
}

