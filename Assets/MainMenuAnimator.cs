using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimator : MonoBehaviour
{
    [SerializeField] GameObject baseButtons;
    [SerializeField] GameObject saveFiles;

    [SerializeField] Animator animator;

    private void Start()
    {
        
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            saveFiles.SetActive(true);
            animator.SetTrigger("ToSaveFiles");
        }
    }
}
