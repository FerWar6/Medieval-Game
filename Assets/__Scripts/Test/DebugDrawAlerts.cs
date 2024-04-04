using UnityEngine;

public class DebugDrawAlerts : MonoBehaviour
{
    private float size = 0.5f;
    private Color color = Color.yellow;

    private void Update()
    {
        DrawCross();
    }

    private void DrawCross()
    {

        Vector3 center = transform.position;

        Vector3 corner1 = center + new Vector3(size, 0, size);
        Vector3 corner2 = center + new Vector3(-size, 0, -size);
        Vector3 corner3 = center + new Vector3(-size, 0, size);
        Vector3 corner4 = center + new Vector3(size, 0, -size);
        Vector3 corner5 = center + new Vector3(0, size, 0);
        Vector3 corner6 = center + new Vector3(0, -size, 0);

        Debug.DrawLine(corner1, corner2, color);
        Debug.DrawLine(corner3, corner4, color);
        Debug.DrawLine(corner5, corner6, color);
    }

}
