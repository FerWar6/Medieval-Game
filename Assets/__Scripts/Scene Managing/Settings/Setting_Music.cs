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
        int input = Mathf.RoundToInt(musicSlider.value);
        int inputSens = Mathf.RoundToInt(input / 2 - 30);

        SettingsManager.instance.musicVolume = inputSens;
        SettingsManager.instance.SaveSettings();
        AudioManager.instance.SetMusicVolume(inputSens);

        musicText.text = input.ToString();
    }
    private void LoadSetting()
    {
        int musicVolume = PlayerPrefs.GetInt(SettingsManager.instance.playerPrefNames[2]);

        int sliderValue = 2 * (musicVolume + 30);

        musicSlider.value = sliderValue;
        musicText.text = (sliderValue).ToString();
    }
}
