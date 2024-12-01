using UnityEngine;

public class PrefabReplacer : MonoBehaviour
{
    [SerializeField] private GameObject prefabToReplace;  // Prefab to replace this GameObject with

    public void ReplacePrefab()
    {
        if (prefabToReplace != null)
        {
            // Replace this object with the prefab
            GameObject newPrefabInstance = Instantiate(prefabToReplace, transform.position, transform.rotation);

            // Destroy the current object
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Prefab to replace is not assigned!");
        }
    }
}
