using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Setting_SoundEffects : MonoBehaviour
{
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    private void Start()
    {
        LoadSetting();
    }
    public void SetSoundEffectVolume()
    {
        int inputSens = Mathf.RoundToInt(soundEffectSlider.value);

        SettingsManager.instance.soundEffectVolume = inputSens;

        soundEffectText.text = SettingsManager.instance.soundEffectVolume.ToString();
    }
    private void LoadSetting()
    {
        soundEffectSlider.value = SettingsManager.instance.soundEffectVolume;
        soundEffectText.text = SettingsManager.instance.soundEffectVolume.ToString();
    }
}
