using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance { get; private set; }

    [HideInInspector] public bool fullscreen = true;

    [HideInInspector] public float playerSens = 0.5f;
    [HideInInspector] public float musicVolume = 0.5f;
    [HideInInspector] public float soundEffectVolume = 0.5f;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
