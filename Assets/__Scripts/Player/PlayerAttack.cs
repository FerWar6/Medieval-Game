using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float attackCoolDown;
    void Update()
    {
        PerformRaycast(transform.forward);
    }
    void PerformRaycast(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null)
            {
            }
            if(hit.collider.gameObject.name == "TestEnemy")
            {
                Debug.Log("hit enemy");
            }
        }
    }
}
