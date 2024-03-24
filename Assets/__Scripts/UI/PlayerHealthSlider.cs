using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthSlider : MonoBehaviour
{
    Slider playerHealthSlider;
    private void Start()
    {
        playerHealthSlider = GetComponent<Slider>();
    }
    private void FixedUpdate()
    {
        playerHealthSlider.value = PlayerData.instance.playerHealth;
    }
}
