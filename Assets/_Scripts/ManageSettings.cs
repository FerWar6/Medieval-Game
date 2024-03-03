using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageSettings : MonoBehaviour
{
    public static float playerSens = 0.5f;

    public static float musicVolume = 0.5f;
    public static float soundEffectVolume = 0.5f;

    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] ResizeWindow ManageWindow;

    private static bool hasRun = false;

    private void Awake()
    {
        if (!hasRun)
        {
            SetDisplayMode(0);
            hasRun = true;
        }
    }

    private void Start()
    {
        resolutionDropdown.onValueChanged.AddListener(SetDisplayMode);
    }
    private void SetDisplayMode(int index)
    {
        switch (index)
        {
            case 0:
                ManageWindow.ChangeWindowMode(ResizeWindow.WindowModes.Fullscreen, Screen.currentResolution.width, Screen.currentResolution.height);
                break;
            case 1:
                ManageWindow.ChangeWindowMode(ResizeWindow.WindowModes.Borderless, Screen.currentResolution.width, Screen.currentResolution.height);
                break;
            case 2:
                ManageWindow.ChangeWindowMode(ResizeWindow.WindowModes.Windowed, Screen.currentResolution.width, Screen.currentResolution.height);
                break;
        }

    }
}
