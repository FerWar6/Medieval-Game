using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Setting_Fullscreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI onOffText;
    [SerializeField] private Image offIndicator;
    [SerializeField] private Image onIndicator;
    private void Start()
    {
        SettingsManager.instance.OnSettingsLoaded.AddListener(LoadSetting);
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
    private void LoadSetting()
    {
        SetFullscreen(SettingsManager.instance.fullscreen);
    }
    private void OnDestroy()
    {
        SettingsManager.instance.OnSettingsLoaded.RemoveListener(LoadSetting);
    }
}
