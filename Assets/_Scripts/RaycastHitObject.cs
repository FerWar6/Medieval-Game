using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHitObject : MonoBehaviour
{
    [SerializeField] Camera camera;

    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Debug.Log("Hit object name: " + objectHit.gameObject.name);

            // Draw a debug ray for visualization
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
        }
    }
}

