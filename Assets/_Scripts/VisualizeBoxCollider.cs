using UnityEngine;

public class VisualizeBoxCollider : MonoBehaviour
{
    [SerializeField]
    private Material visualizationMaterial;
    private GameObject visualMeshObject;

    private BoxCollider boxCollider;

    void Start()
    {
        // Get the BoxCollider component once at the start
        boxCollider = GetComponent<BoxCollider>();

        // Create the visual mesh object
        CreateVisualMeshObject();
    }

    private void CreateVisualMeshObject()
    {
        // Create a new GameObject for the visual mesh
        visualMeshObject = new GameObject("VisualMesh");
        visualMeshObject.transform.parent = transform;
        visualMeshObject.transform.localPosition = Vector3.zero;
        visualMeshObject.transform.localRotation = Quaternion.identity;

        // Attach a MeshFilter and MeshRenderer to the visual mesh object
        MeshFilter meshFilter = visualMeshObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = visualMeshObject.AddComponent<MeshRenderer>();

        // Create the visual mesh
        meshFilter.mesh = CreateVisualMesh();

        // Set the visualization material
        meshRenderer.material = visualizationMaterial;
    }

    private Mesh CreateVisualMesh()
    {
        Mesh mesh = new Mesh();

        if (boxCollider != null)
        {
            // Calculate the corner points of the collider
            Vector3 center = boxCollider.center;
            Vector3 size = boxCollider.size;
            Vector3 halfSize = size * 0.5f;

            Vector3[] vertices = new Vector3[]
            {
                center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z),
                center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z),
                center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z),
                center + new Vector3(halfSize.x, -halfSize.y, halfSize.z),
                center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z),
                center + new Vector3(halfSize.x, halfSize.y, -halfSize.z),
                center + new Vector3(-halfSize.x, halfSize.y, halfSize.z),
                center + new Vector3(halfSize.x, halfSize.y, halfSize.z)
            };

            int[] triangles = new int[]
            {
                0, 1, 2, 2, 1, 3,
                4, 6, 5, 6, 7, 5,
                0, 2, 4, 2, 6, 4,
                1, 5, 3, 5, 7, 3,
                0, 4, 1, 4, 5, 1,
                2, 3, 6, 6, 3, 7
            };

            mesh.vertices = vertices;
            mesh.triangles = triangles;
        }

        return mesh;
    }
}
