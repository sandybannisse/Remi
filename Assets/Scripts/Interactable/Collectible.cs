using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public static event Action OnCollected;
    void Update()
    {
        // Get the object's MeshFilter
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            // Calculate the local center offset
            Vector3 localCenter = meshFilter.mesh.bounds.center;

            // Translate to center, rotate, and then translate back
            transform.localPosition -= localCenter; // Translate to center
            transform.localRotation = Quaternion.Euler(0f, Time.time * 100f, 0f); // Apply rotation
            transform.localPosition += localCenter; // Translate back
        }
        else
        {
            Debug.LogWarning("No MeshFilter found. Ensure this script is attached to an object with a mesh.");
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
