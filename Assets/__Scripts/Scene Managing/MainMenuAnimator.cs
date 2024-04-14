using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimator : MonoBehaviour
{
    [SerializeField] GameObject baseButtons;
    [SerializeField] GameObject saveFiles;

    private Animator anim;
    
    private bool onBaseButtons = true;
    private void Start()
    {
        anim = GetComponent<Animator>();
        saveFiles.SetActive(false);
    }
    public void _PlayButtonTwistAnimation()
    {
        if (onBaseButtons)
        {
            anim.Play("ToBaseButtons"); 
        }
        if (!onBaseButtons)
        {

            anim.Play("ToSaveFiles");
        }
        onBaseButtons = !onBaseButtons;
    }
    public void SetBaseButtonsActive()
    {
        saveFiles.SetActive(true);
        baseButtons.SetActive(false);
    }
    public void SetSaveFilesActive()
    {
        saveFiles.SetActive(false);
        baseButtons.SetActive(true);
    }
    public void SetAllActive()
    {
        saveFiles.SetActive(true);
        baseButtons.SetActive(true);
    }
}
