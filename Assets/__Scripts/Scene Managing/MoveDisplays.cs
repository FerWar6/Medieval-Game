using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MoveDisplays : MonoBehaviour
{
    private float transitionSpeed = 20f;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject deathMenu;
    private Transform mainCam;
    private void Start()
    {
        mainCam = Camera.main.transform;
        _SetDeathScreen(SettingsManager.instance.deathScreenOn);
    }
    public void _SetDeathScreen(bool deathScreen)
    {
        switch (deathScreen)
        {
            case true:
            {
                mainMenu.SetActive(false);
                settingsMenu.SetActive(false);
                deathMenu.SetActive(true);
                break;
            }
            case false:
            {
                mainMenu.SetActive(true);
                settingsMenu.SetActive(true);
                deathMenu.SetActive(false);
                break;
            }
        }
    }
    public void _MoveMenu(int index)
    {
        switch (index)
        {
            case 1:
                {
                    Vector3 targetPos = new Vector3(mainCam.transform.position.x - 37.85f, mainCam.transform.position.y, mainCam.transform.position.z);
                    StartCoroutine(MoveObject(targetPos));

                    break;
                }
            case 2:
                {
                    Vector3 targetPos = new Vector3(mainCam.transform.position.x + 37.85f, mainCam.transform.position.y, mainCam.transform.position.z);

                    StartCoroutine(MoveObject(targetPos));

                    break;
                }
        }
    }

    IEnumerator MoveObject(Vector3 targetPosition)
    {
        Vector3 initialPosition = mainCam.position;
        float distance = Vector3.Distance(initialPosition, targetPosition);
        float duration = distance / transitionSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = EaseInOutQuart(t); // Apply easing
            mainCam.position = Vector3.Lerp(initialPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCam.position = targetPosition;
    }

    float EaseInOutQuart(float x)
    {
        return x < 0.5f ? 8f * x * x * x * x : 1f - Mathf.Pow(-2f * x + 2f, 4f) / 2f;
    }

}
