using UnityEngine;

public class RaycastHitObject : MonoBehaviour
{
    [SerializeField] Camera camera;

    void FixedUpdate()
    {
        // Get the center of the camera's viewport (middle of the screen)
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit hit;
        Ray ray = new Ray(rayOrigin, camera.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Debug.Log("Hit object name: " + objectHit.gameObject.name);

            // Draw a debug ray for visualization
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
        }
    }
}
