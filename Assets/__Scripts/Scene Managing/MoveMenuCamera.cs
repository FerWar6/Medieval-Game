using System.Collections;
using UnityEngine;

public class MoveMenuCamera : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject deathScreen;

    private Transform mainCam;
    private void Start()
    {
        mainCam = Camera.main.transform;
        if (SettingsManager.instance.deathScreenOn)
        {
            SetMenus(false, false, true);
            Vector3 targetPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - 12.4f, mainCam.transform.position.z);
            mainCam.position = targetPos;
            SettingsManager.instance.deathScreenOn = false;
        }
        else
        {
            SetMenus(true, false, false);
        }
    }
    public void _MoveMenu(int location)
    {
        if (location == 1)
        {
            SetMenus(true, true, false);
            Vector3 targetPos = new Vector3(mainCam.transform.position.x - 37.85f, mainCam.transform.position.y, mainCam.transform.position.z);
            StartCoroutine(MoveObject(targetPos, false, true, false, 22));
        }
        if (location == 2)
        {
            SetMenus(true, true, false);
            Vector3 targetPos = new Vector3(mainCam.transform.position.x + 37.85f, mainCam.transform.position.y, mainCam.transform.position.z);
            StartCoroutine(MoveObject(targetPos, true, false, false, 22));
        }
        if (location == 3)
        {
            SetMenus(true, false, true);
            Vector3 targetPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + 12.4f, mainCam.transform.position.z);
            StartCoroutine(MoveObject(targetPos, true, false, false, 10));
        }

    }

    IEnumerator MoveObject(Vector3 targetPosition, bool main, bool settings, bool death, float speed)
    {
        Vector3 initialPosition = mainCam.position;
        float distance = Vector3.Distance(initialPosition, targetPosition);
        float duration = distance / speed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float curveValue = curve.Evaluate(t);
            mainCam.position = Vector3.Lerp(initialPosition, targetPosition, curveValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCam.position = targetPosition;
        SetMenus(main, settings, death);
    }
    private void SetMenus(bool main, bool settings, bool death)
    {
        mainMenu.SetActive(main);
        settingsMenu.SetActive(settings);
        deathScreen.SetActive(death);
    }
}
