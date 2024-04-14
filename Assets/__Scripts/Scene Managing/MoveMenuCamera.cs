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
        SettingsManager.instance.OnSettingsLoaded.AddListener(CheckForDeathScreen);

        mainCam = Camera.main.transform;
    }
    private void CheckForDeathScreen()
    {
        int startScreenIndex = SettingsManager.instance.startScreenIndex;
        switch(startScreenIndex)
        {
            case 0:
                {
                    // Load Main Menu
                    SetMenus(true, false, false);
                    break;
                }
            case 1:
                {
                    // Load Settings Menu
                    SetMenus(false, true, false);
                    Vector3 targetPos = new Vector3(mainCam.transform.position.x - 37.85f, mainCam.transform.position.y, mainCam.transform.position.z);
                    mainCam.position = targetPos;
                    break;
                }
            case 2:
                {
                    // Load Death Screen
                    SetMenus(false, false, true);
                    Vector3 targetPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - 12.4f, mainCam.transform.position.z);
                    mainCam.position = targetPos;
                    break;
                }
        }
    }
    public void _MoveMenu(int location)
    {
        switch (location)
        {
            case 1:
                {
                    //Move Camera from main menu to settings menu
                    SetMenus(true, true, false);
                    Vector3 targetPos = new Vector3(mainCam.transform.position.x - 37.85f, mainCam.transform.position.y, mainCam.transform.position.z);
                    StartCoroutine(MoveObject(targetPos, false, true, false, 22));
                    break;
                }
            case 2:
                {
                    //Move Camera from settings menu to main menu
                    SetMenus(true, true, false);
                    Vector3 targetPos = new Vector3(mainCam.transform.position.x + 37.85f, mainCam.transform.position.y, mainCam.transform.position.z);
                    StartCoroutine(MoveObject(targetPos, true, false, false, 22));
                    break;
                }
            case 3:
                {
                    //Move Camera from death screen to main menu
                    SetMenus(true, false, true);
                    Vector3 targetPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + 12.4f, mainCam.transform.position.z);
                    StartCoroutine(MoveObject(targetPos, true, false, false, 10));
                    break;
                }
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
    private void OnDestroy()
    {
        SettingsManager.instance.OnSettingsLoaded.RemoveListener(CheckForDeathScreen);
    }
}
