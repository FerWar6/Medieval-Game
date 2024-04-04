using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackProjectile : MonoBehaviour
{
    [SerializeField] AudioClip hitSound;

    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] LayerMask enemyLayerMask;

    private float maxLifetime = 5;
    private float lifeTime = 0;
    private bool lockProjectile = false;
    private bool ableToSetLastVariables = true;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(lifeTime < maxLifetime)
        {
            lifeTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((groundLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            if (hitSound != null)
            {
                PlaySound();
            }
            LockProjectile();
            Destroy(gameObject);
        }
        if ((enemyLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            if (hitSound != null)
            {
                PlaySound();
            }
            LockProjectile();
            collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(40);
            Destroy(gameObject);
        }
    }

    private IEnumerator PlayParticleAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }


    private IEnumerator SetLastVariables(Quaternion rotation, Transform transform)
    {
        ableToSetLastVariables = false;
        yield return new WaitForSeconds(0.02f);
        ableToSetLastVariables = true;
    }
    private void LockProjectile()
    {
        lockProjectile = true;
        GetComponent<SphereCollider>().enabled = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
    }
    private void PlaySound()
    {
        AudioManager.instance.SetAudioClip(hitSound, transform.position);
    }
}
