using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Setting_Music : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicText;
    private void Start()
    {
        LoadSetting();
    }
    public void SetMusicVolume()
    {
        int inputSens = Mathf.RoundToInt(musicSlider.value);

        SettingsManager.instance.musicVolume = inputSens;

        musicText.text = SettingsManager.instance.musicVolume.ToString();
    }
    private void LoadSetting()
    {
        musicSlider.value = SettingsManager.instance.musicVolume;
        musicText.text = SettingsManager.instance.musicVolume.ToString();
    }
}
