using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance { get; private set; }

    public bool gamePaused = false;

    public bool fullscreen = true;

    public float playerSens = 0.98f;
    public int musicVolume = 40;
    public int soundEffectVolume = 40;

    public float playerFOV = 60f;


    private void Awake()
    {
        Cursor.visible = true;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SetPlayerFOV(float fov)
    {
        playerFOV = fov;
    }
    public void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        Cursor.visible = false;

    }
}
