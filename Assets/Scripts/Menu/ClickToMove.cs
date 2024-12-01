using System.Collections;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    public float clickDistance = 0.2f; // Distance to move down
    public float returnDelay = 0.2f;  // Time before moving back up

    private Vector3 originalPosition;

    void Start()
    {
        // Store the original position of the object
        originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        // Start the coroutine to handle the movement
        StartCoroutine(MoveDownAndUp());
    }

    private IEnumerator MoveDownAndUp()
    {
        // Move the object downward
        transform.position = originalPosition + Vector3.down * clickDistance;

        // Wait for the specified delay
        yield return new WaitForSeconds(returnDelay);

        // Move the object back to its original position
        transform.position = originalPosition;
    }
}
