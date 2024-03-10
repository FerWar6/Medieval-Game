using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageSettings : MonoBehaviour
{
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Slider sensSlider;

    private void Start()
    {
        sensSlider.onValueChanged.AddListener(ChangePlayerSens);
    }



    public void ChangePlayerSens(float value)
    {

        float speedValue = sensSlider.value;
        PlayerPrefs.SetFloat("Speed", speedValue);

    }
    private void Update()
    {
        if (SettingsManager.instance.playerSens == 0.5f)
        {

        }
    }
}
