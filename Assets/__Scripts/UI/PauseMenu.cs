using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!SettingsManager.instance.gamePaused)
            {
                pauseMenu.SetActive(true);
                SettingsManager.instance.PauseGame();
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                pauseMenu.SetActive(false);
                SettingsManager.instance.ResumeGame();
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

    }
    private void OnDisable()
    {
        SettingsManager.instance.ResumeGame();
    }
    public void UnPause()
    {
        pauseMenu.SetActive(false);
        SettingsManager.instance.ResumeGame();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
