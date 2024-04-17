using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonCellSwordAttack : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            anim.SetTrigger("Attack");
        }
    }
}
