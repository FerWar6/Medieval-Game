using System.Collections;
using UnityEngine;

public class BoneSkullLight : MonoBehaviour
{
    private Light lightSource;

    private float lightUpDuration = 3f;
    private float waitBetween = 1f;
    private float lightReturnDuration = 0.65f;
    private void Start()
    {
        lightSource = GetComponent<Light>();
        StartCoroutine(RandomLightControl());
    }

    private IEnumerator RandomLightControl()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            int randomNumber;
            randomNumber = Random.Range(0, 30);
            if (randomNumber == 5)
            {
                yield return StartCoroutine(LerpLightRange(0.1f, 3, lightUpDuration));

                yield return new WaitForSeconds(waitBetween);

                yield return StartCoroutine(LerpLightRange(3, 0.1f, lightReturnDuration));
            }
        }
    }

    private IEnumerator LerpLightRange(float startRange, float endRange, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            lightSource.range = Mathf.Lerp(startRange, endRange, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lightSource.range = endRange;
    }
}
