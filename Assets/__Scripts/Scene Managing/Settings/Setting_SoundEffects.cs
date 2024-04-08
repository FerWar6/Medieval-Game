using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Setting_SoundEffects : MonoBehaviour
{
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    private void Start()
    {
        SettingsManager.instance.OnSettingsLoaded.AddListener(LoadSetting);
    }
    public void SetSoundEffectVolume()
    {
        int input = Mathf.RoundToInt(soundEffectSlider.value);
        int inputSens = Mathf.RoundToInt(input / 2 - 30);

        SettingsManager.instance.soundEffectVolume = inputSens;
        AudioManager.instance.SetSoundEffectVolume(inputSens);

        soundEffectText.text = input.ToString();
    }
    private void LoadSetting()
    {
        int sliderValue = 2 * (SettingsManager.instance.soundEffectVolume + 30);
        soundEffectSlider.value = sliderValue;
        soundEffectText.text = (sliderValue).ToString();
    }
    private void OnDestroy()
    {
        SettingsManager.instance.OnSettingsLoaded.RemoveListener(LoadSetting);
    }
}
