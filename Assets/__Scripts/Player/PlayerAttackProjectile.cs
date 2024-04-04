using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackProjectile : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem trail;

    [SerializeField] AudioClip hitSound;
    [SerializeField] TrailAnimation trailAnim;

    [SerializeField] LayerMask everythingNotPlayerLayerMask;
    [SerializeField] LayerMask enemyLayerMask;

    private float maxLifetime = 2.5f;
    private float lifeTime = 0;
    private bool collided = true;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (!collided)
        {
            if (lifeTime < maxLifetime)
            {
                lifeTime += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((everythingNotPlayerLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            collided = true;
            if (hitSound != null)
            {
                PlaySound();
            }
            trail.Stop();

            LockProjectile();
            StartCoroutine(PlayParticleAndDestroy());
        }
        if ((enemyLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            collided = true;
            if (hitSound != null)
            {
                PlaySound();
            }
            trail.Stop();

            LockProjectile();
            collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(40);
            StartCoroutine(PlayParticleAndDestroy());
        }
    }

    private IEnumerator PlayParticleAndDestroy()
    {
        explosion.Play();
        yield return new WaitUntil(() => !explosion.isPlaying);
        Destroy(gameObject);
    }
    private void LockProjectile()
    {
        trailAnim.locked = true;
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
