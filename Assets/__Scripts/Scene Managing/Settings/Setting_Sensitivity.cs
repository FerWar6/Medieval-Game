using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Setting_Sensitivity : MonoBehaviour
{
    [SerializeField] private Slider sensSlider;
    [SerializeField] private TextMeshProUGUI sensText;
    private void Start()
    {
        LoadSetting();
    }
    public void SetSensitivity()
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
        SettingsManager.instance.SaveSettings();

    }
    private void LoadSetting()
    {
        float settingValue = SettingsManager.instance.playerSens;

        sensText.text = settingValue.ToString();

        if (settingValue >= 0 && settingValue <= 1)
        {
            sensSlider.value = settingValue * 50;
        }
        else if (settingValue >= 1.1 && settingValue <= 10)
        {
            sensSlider.value = ((settingValue - 1) * 10) + 50;
        }
        else if (settingValue >= 11 && settingValue <= 20)
        {
            sensSlider.value = settingValue + 130;
        }

        settingValue = Mathf.Round(settingValue * 100f) / 100f;

        if (settingValue < 1f)
        {
            sensText.text = settingValue.ToString("F2");
        }
        else
        {
            sensText.text = settingValue.ToString("F1");
        }
    }
}
