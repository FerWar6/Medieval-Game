using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageSettings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI onOffText;
    [SerializeField] private Image offIndicator;
    [SerializeField] private Image onIndicator;
    [SerializeField] private Image leftArrow;
    [SerializeField] private Image rightArrow;

    [SerializeField] private Slider sensSlider;
    [SerializeField] private TextMeshProUGUI sensText;


    private void Start()
    {
        SetFullscreen(SettingsManager.instance.fullscreen);
    }
    public void SetFullscreen(bool input)
    {
        if (input)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            SettingsManager.instance.fullscreen = true;
            onOffText.text = "ON";
            offIndicator.enabled = false;
            onIndicator.enabled = true;
        }
        else if (!input)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
            SettingsManager.instance.fullscreen = false;
            onOffText.text = "OFF";
            offIndicator.enabled = true;
            onIndicator.enabled = false;
        }
    }
    public void ShowArrows()
    {
        if (leftArrow.enabled == true)
        {
            leftArrow.enabled = false;
            rightArrow.enabled = false;
        }
        else if (leftArrow.enabled == false)
        {
            leftArrow.enabled = true;
            rightArrow.enabled = true;
        }
    }
    public void SetSensText()
    {
        int inputSens = Mathf.RoundToInt(sensSlider.value);

        if (inputSens >= 0 && inputSens <= 50)
        {
            SettingsManager.instance.playerSens = inputSens * 0.02f;
        }
        else if (inputSens >= 51 && inputSens <= 140)
        {
            SettingsManager.instance.playerSens = ((inputSens - 50) * 0.1f) + 1f;
        }
        else if (inputSens >= 141 && inputSens <= 150)
        {
            SettingsManager.instance.playerSens = ((inputSens - 140) * 1f) + 10f;
        }

        SettingsManager.instance.playerSens = Mathf.Round(SettingsManager.instance.playerSens * 100f) / 100f;

        if (SettingsManager.instance.playerSens < 1f)
        {
            sensText.text = SettingsManager.instance.playerSens.ToString("F2");
        }
        else
        {
            sensText.text = SettingsManager.instance.playerSens.ToString("F1");
        }
    }
}
