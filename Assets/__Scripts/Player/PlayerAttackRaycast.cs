using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
public class PlayerAttackRaycast : MonoBehaviour
{
    [SerializeField] GoldenBallAnimation ballAnimator;
    private bool canFire = true;

    public LayerMask enemyLayerMask;
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
        yield return new WaitForSeconds(0.05f);
        Fire();
        canFire = true;
    }

    private void Fire()
    {
        PerformRaycast(Camera.main.transform.forward);
    }
    void PerformRaycast(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
        {

            ballAnimator.SetEndPosition(hit.point);

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
}
