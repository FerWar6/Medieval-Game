using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageSettings : MonoBehaviour
{



    [SerializeField] private Slider sensSlider;
    [SerializeField] private TextMeshProUGUI sensText;

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
