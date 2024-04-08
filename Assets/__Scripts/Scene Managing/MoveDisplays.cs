using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MoveDisplays : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;

    [SerializeField] float transitionSpeed = 20f;

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
        if (deathScreen)
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(false);
            deathMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(true);
            deathMenu.SetActive(false);
        }
    }

    public void _MoveMenu(bool goLeft)
    {
        if (goLeft)
        {
            Vector3 targetPos = new Vector3(mainCam.transform.position.x - 37.85f, mainCam.transform.position.y, mainCam.transform.position.z);
            StartCoroutine(MoveObject(targetPos));
        }
        else
        {
            Vector3 targetPos = new Vector3(mainCam.transform.position.x + 37.85f, mainCam.transform.position.y, mainCam.transform.position.z);
            StartCoroutine(MoveObject(targetPos));
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
            float curveValue = curve.Evaluate(t); // Evaluate curve at current time
            mainCam.position = Vector3.Lerp(initialPosition, targetPosition, curveValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCam.position = targetPosition;
    }
}
