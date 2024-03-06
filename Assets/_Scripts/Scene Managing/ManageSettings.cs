using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageSettings : MonoBehaviour
{
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Slider sensSlider;

    private void Start()
    {
        SetFullscreen(SettingsManager.instance.fullscreen);
        fullscreenToggle.isOn = SettingsManager.instance.fullscreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        sensSlider.onValueChanged.AddListener(ChangePlayerSens);
    }
    private void SetFullscreen(bool fullscreen)
    {
        if (fullscreen) Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else Screen.fullScreenMode = FullScreenMode.Windowed;
    }
    public void ChangePlayerSens(float value)
    {

        float speedValue = sensSlider.value;
        PlayerPrefs.SetFloat("Speed", speedValue);

    }
    private void Update()
    {
        if (SettingsManager.instance.playerSens == 0.5f)
        {

        }
    }
}
