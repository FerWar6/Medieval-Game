using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 lookAtPosition = new Vector3(cameraPosition.x, transform.position.y, cameraPosition.z);
        Vector3 direction = lookAtPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
