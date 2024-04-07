using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance { get; private set; }

    public bool deathScreenOn = false;
    public bool gamePaused = false;

    public bool fullscreen = true;

    [Range(0, 20)]
    public float playerSens = 0.98f;
    [Range(-30, 20)]
    public int musicVolume = 40;
    [Range(-30, 20)]
    public int soundEffectVolume = 40;

    public float playerFOV = 60f;

    public List<string> playerPrefNames = new List<string>();

    private void Awake()
    {
        //LoadSettings();
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
    private void Start()
    {
        LoadSettings();
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
    public void SaveSettings()
    {
        PlayerPrefs.SetInt(playerPrefNames[0], fullscreen ? 1 : 0);
        PlayerPrefs.SetFloat(playerPrefNames[1], playerSens);
        PlayerPrefs.SetInt(playerPrefNames[2], musicVolume);
        PlayerPrefs.SetInt(playerPrefNames[3], soundEffectVolume);
    }
    private void LoadSettings()
    {
        fullscreen = PlayerPrefs.GetInt(playerPrefNames[0]) != 0 ? true : false;
        playerSens = PlayerPrefs.GetFloat(playerPrefNames[1]);
        musicVolume = PlayerPrefs.GetInt(playerPrefNames[2]);
        soundEffectVolume = PlayerPrefs.GetInt(playerPrefNames[3]);

        AudioManager.instance.SetMusicVolume(musicVolume);
        AudioManager.instance.SetSoundEffectVolume(soundEffectVolume);

        /*
                AudioManager.instance.mixer.SetFloat("Music Volume", musicVolume);
                AudioManager.instance.mixer.SetFloat("Sound Effect Volume", soundEffectVolume);*/
    }
    void OnApplicationQuit()
    {
        SaveSettings();
    }
}
