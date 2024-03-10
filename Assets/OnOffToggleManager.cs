using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class OnOffToggleManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI onOffText;
    [SerializeField] private Image offIndicator;
    [SerializeField] private Image onIndicator;

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
}
