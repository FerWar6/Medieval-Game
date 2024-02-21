using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float mouseSens = 2f;
    float cameraVericalRotation = 0;

    bool lockedCursor = true;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSens;
        float inputY = Input.GetAxis("Mouse Y") * mouseSens;

        cameraVericalRotation -= inputY;
        cameraVericalRotation = Mathf.Clamp(cameraVericalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVericalRotation;

        player.Rotate(Vector3.up * inputX);
    }
}
