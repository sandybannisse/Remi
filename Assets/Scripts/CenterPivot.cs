using UnityEngine;

public class AdjustPivot : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            // Clone the mesh to avoid modifying the shared prefab mesh
            Mesh mesh = Instantiate(meshFilter.sharedMesh);
            meshFilter.mesh = mesh;

            // Calculate the offset for centering the pivot
            Vector3 offset = mesh.bounds.center;

            // Move the object itself to keep its visual position consistent
            transform.position += transform.TransformDirection(offset);

            // Adjust vertices to offset the pivot
            Vector3[] vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] -= offset;
            }

            // Apply changes to the mesh
            mesh.vertices = vertices;
            mesh.RecalculateBounds();
        }
        else
        {
            Debug.LogWarning("No MeshFilter found. Ensure this script is attached to an object with a mesh.");
        }
    }
}


