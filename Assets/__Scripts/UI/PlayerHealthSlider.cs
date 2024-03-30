using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthSlider : MonoBehaviour
{
    Slider healthBar;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        UpdateHealthBar();
        PlayerData.instance.OnHealthChanged.AddListener(UpdateHealthBar);
    }

    private void OnDestroy()
    {
        PlayerData.instance.OnHealthChanged.RemoveListener(UpdateHealthBar);
    }

    private void UpdateHealthBar()
    {
        healthBar.value = PlayerData.instance.playerHealth;
    }
}