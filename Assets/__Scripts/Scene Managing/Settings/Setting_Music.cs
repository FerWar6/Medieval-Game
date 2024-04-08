using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Setting_Music : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicText;
    private void Start()
    {
        SettingsManager.instance.OnSettingsLoaded.AddListener(LoadSetting);
    }
    public void SetMusicVolume()
    {
        int input = Mathf.RoundToInt(musicSlider.value);
        int inputSens = Mathf.RoundToInt(input / 2 - 30);

        SettingsManager.instance.musicVolume = inputSens;
        AudioManager.instance.SetMusicVolume(inputSens);

        musicText.text = input.ToString();
    }
    private void LoadSetting()
    {
        int sliderValue = 2 * (SettingsManager.instance.musicVolume + 30);

        musicSlider.value = sliderValue;
        musicText.text = (sliderValue).ToString();
    }
    private void OnDestroy()
    {
        SettingsManager.instance.OnSettingsLoaded.RemoveListener(LoadSetting);
    }
}
