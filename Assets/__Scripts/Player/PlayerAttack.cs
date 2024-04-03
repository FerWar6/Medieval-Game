using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] AudioClip hitSound;
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
        yield return new WaitForSeconds(0.5f);
        Fire();
        canFire = true;
    }

    private void Fire()
    {
        ShootFireBall(Camera.main.transform.forward);
        if (hitSound != null)
        {
            PlaySound();
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
        Vector3 newPos = Camera.main.transform.position;
        GameObject newProjectile = Instantiate(projectile, newPos, Quaternion.identity);

        newProjectile.transform.rotation = Camera.main.transform.rotation;

        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }
    private void PlaySound()
    {
        //AudioManager.instance.SetAudioClip(hitSound, transform.position);
    }
}
