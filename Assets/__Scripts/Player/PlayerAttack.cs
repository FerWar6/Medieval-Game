using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] List <AudioClip> fireBallSounds;
    [SerializeField] Transform projectilePool;
    [SerializeField] Animator fireballAnim;

    [SerializeField] GameObject projectile;
    private bool canFire = true;

    //public LayerMask enemyLayerMask;
    [SerializeField] float attackCooldown;
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed && canFire)
        {
            StartCoroutine(FireWithDelay());
        }
    }

    IEnumerator FireWithDelay()
    {
        canFire = false;
        Fire();

        yield return new WaitForSeconds(0.5f);
        fireballAnim.Play("Idle");
        canFire = true;
    }

    private void Fire()
    {
        ShootFireBall(Camera.main.transform.forward);
        fireballAnim.SetTrigger("regenFireball");
        if (fireBallSounds != null)
        {
            //PlayFireballSound();
        }
    }
    void PerformRaycast(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
        {
            if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Enemy")
            {
                EnemyBehaviour enemyBehaviour = hit.collider.GetComponent<EnemyBehaviour>();
                if (enemyBehaviour != null)
                {
                    enemyBehaviour.TakeDamage(40);
                    Debug.Log("Enemy in sight. Dealt damage to enemy.");
                }
            }
        }
    }
    void ShootFireBall(Vector3 direction)
    {
        Transform camTrans = Camera.main.transform;
        GameObject newProjectile = Instantiate(projectile, camTrans.position, camTrans.rotation, projectilePool);

        newProjectile.transform.rotation = Camera.main.transform.rotation;

        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

        rb.AddForce(camTrans.forward * 25f, ForceMode.Impulse);
    }

    private void PlayFireballSound()
    {
        if(fireBallSounds != null)
        {
            int random = Random.Range(1, 4);
            switch (random)
            {
                case 1:
                    {
                        AudioManager.instance.SetAudioClip(fireBallSounds[0], transform.position);
                        break;
                    }
                case 2:
                    {
                        AudioManager.instance.SetAudioClip(fireBallSounds[1], transform.position);
                        break;
                    }
                case 3:
                    {
                        AudioManager.instance.SetAudioClip(fireBallSounds[2], transform.position);
                        break;
                    }
            }
        }

    }
}
