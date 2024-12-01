using UnityEngine;

public class RayCasAlignnerNoOverlP : MonoBehaviour
{
    public GameObject[] itemsToPickFrom;
    public float spawnHeight = 100f;           // Height above the ground to spawn items
    public LayerMask spawnedObjectLayer;      // Layer mask to identify spawned objects

    void Start() 
    {
        PositionCast();  
    }

    void PositionCast()  
    {
        RaycastHit hit;  
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            
            Pick(hit.point, spawnRotation);
        }
    }

    void Pick(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length);
        
        // Spawn the item at a height above the ground
        Vector3 spawnPosition = positionToSpawn + Vector3.up * spawnHeight;
        GameObject clone = Instantiate(itemsToPickFrom[randomIndex], spawnPosition, rotationToSpawn);
        
        // Ensure the item has a Rigidbody component to allow it to fall with gravity
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = clone.AddComponent<Rigidbody>();
        }
        
        // Set Rigidbody properties for smoother falling behavior
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Helps prevent fast objects from passing through the floor
        rb.mass = 1; // Adjust if needed for more realistic falling

        // Optional: freeze rotation if you don't want the objects to tip over when they fall
        rb.freezeRotation = true;
    }
}
