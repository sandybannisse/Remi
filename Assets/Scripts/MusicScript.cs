using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField] public AK.Wwise.Event Music;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Music.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
