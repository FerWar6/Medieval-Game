using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    public Camera cam;
    [SerializeField] Transform player;
    [HideInInspector] float mouseSens = 1;
    float cameraVerticalRotation = 0;

    void Start()
    {
       // mouseSens = SettingsManager.instance.playerSens;
        cam = GetComponent<Camera>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam.transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float inputX = mouseDelta.x * mouseSens;
        float inputY = mouseDelta.y * mouseSens;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = new Vector3(cameraVerticalRotation, 0, 0);

        player.Rotate(Vector3.up * inputX);
    }
    private void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
