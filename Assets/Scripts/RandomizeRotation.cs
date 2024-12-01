using UnityEngine;

public class RandomizeRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeYRotation();
    }

    void RandomizeMyRotation() 
    {
        transform.rotation = Random.rotation;
    }

    void RandomizeYRotation() 
    {
        Quaternion randomYRotation = Quaternion.Euler(0,Random.Range(0,360),0);
        transform.rotation = randomYRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
