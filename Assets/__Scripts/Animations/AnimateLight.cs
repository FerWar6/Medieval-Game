using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateLight : MonoBehaviour
{
    private Light light;

    private float minIntensity = 1f;
    [SerializeField] float maxIntensity = 5f;
    private float pingPongSpeed = 0.5f;

    private bool startAnimation = false;
    private void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(StartAnimationWithDelay());
    }

    private IEnumerator StartAnimationWithDelay()
    {
        yield return new WaitForSeconds((Random.Range(1, 1001) / 100));
        startAnimation = true;
    }

    void Update()
    {
        if (startAnimation)
        {
            float intensity = Mathf.PingPong(Time.time * pingPongSpeed, maxIntensity - minIntensity) + minIntensity;
            light.intensity = intensity;
        }
    }
}
