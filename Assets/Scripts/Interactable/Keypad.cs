using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Keypad : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        Debug.Log("Interacted with" + gameObject.name);
    }
}