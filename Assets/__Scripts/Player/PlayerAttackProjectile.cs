using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackProjectile : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
