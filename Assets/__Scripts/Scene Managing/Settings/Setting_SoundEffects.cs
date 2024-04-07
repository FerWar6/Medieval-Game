using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Setting_SoundEffects : MonoBehaviour
{
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    private void Awake()
    {
        LoadSetting();
    }
    public void SetSoundEffectVolume()
    {
        int input = Mathf.RoundToInt(soundEffectSlider.value);
        int inputSens = Mathf.RoundToInt(input / 2 - 30);

        SettingsManager.instance.soundEffectVolume = inputSens;
        SettingsManager.instance.SaveSettings();
        AudioManager.instance.SetSoundEffectVolume(inputSens);

        soundEffectText.text = input.ToString();
    }
    private void LoadSetting()
    {
        int soundEffectVolume = PlayerPrefs.GetInt(SettingsManager.instance.playerPrefNames[3]);

        int sliderValue = 2 * (soundEffectVolume + 30);

        soundEffectSlider.value = sliderValue;
        soundEffectText.text = (sliderValue).ToString();
    }
}
