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
        if (fullscreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        else
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
        }
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
