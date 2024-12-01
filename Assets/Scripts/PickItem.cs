using UnityEngine;

public class PickItem : MonoBehaviour 
{
    public GameObject[] itemsToPickFrom;
    public float spawnHeight = 10f; // Height above ground to spawn items

    void Start()
    {
        Pick();
    }

    void Pick()
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length);
        
        // Set spawn position above ground to let items fall
        Vector3 spawnPosition = transform.position + Vector3.up * spawnHeight;
        GameObject clone = Instantiate(itemsToPickFrom[randomIndex], spawnPosition, Quaternion.identity);

        // Ensure item has a Rigidbody for gravity
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = clone.AddComponent<Rigidbody>();
            rb.useGravity = true;

        }

        rb.mass = 100; // Set the weight (mass) of the item to 100
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
        // Apply additional properties to reduce wobbling
        rb.freezeRotation = true;
        rb.linearDamping = 1;           // Increase linear drag for smoother settling
        rb.angularDamping = 5;     // Increase angular drag to reduce wobbling
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // Optional adjustment to stabilize

        // Check if the item has a MeshCollider and set it to convex if needed
        MeshCollider meshCollider = clone.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            meshCollider.convex = true; // Convex is required for Rigidbody to work with MeshCollider
        }
        else if (clone.GetComponent<Collider>() == null)
        {
            // Add a default BoxCollider if no collider exists
            clone.AddComponent<BoxCollider>();
        }
    }
}
