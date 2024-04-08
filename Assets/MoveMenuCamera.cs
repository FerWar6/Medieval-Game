using System.Collections;
using UnityEngine;

public class MoveMenuCamera : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject deathScreen;

    private float transitionSpeed = 30f;
    private Transform mainCam;
    private void Start()
    {
        mainCam = Camera.main.transform;
        if (SettingsManager.instance.deathScreenOn)
        {
            Vector3 targetPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - 12.4f, mainCam.transform.position.z);

            mainCam.position = targetPos;
        }
    }
    public void _MoveMenu(int location)
    {
        if (location == 1)
        {
            Vector3 targetPos = new Vector3(mainCam.transform.position.x - 37.85f, mainCam.transform.position.y, mainCam.transform.position.z);
            StartCoroutine(MoveObject(targetPos));
        }
        if (location == 2)
        {
            Vector3 targetPos = new Vector3(mainCam.transform.position.x + 37.85f, mainCam.transform.position.y, mainCam.transform.position.z);
            StartCoroutine(MoveObject(targetPos));
        }
        if (location == 3)
        {
            Vector3 targetPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + 12.4f, mainCam.transform.position.z);
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
            float curveValue = curve.Evaluate(t);
            mainCam.position = Vector3.Lerp(initialPosition, targetPosition, curveValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCam.position = targetPosition;
    }
}
