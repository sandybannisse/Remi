using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationCollect : Interactable
{
    Animator animator;
    private string startPrompt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        startPrompt = promptMessage; 
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            promptMessage = startPrompt;
        }
        else {
            promptMessage = "Animating";
        }
    }

    protected override void Interact()
    {
        //Debug.Log("Interacted with" + gameObject.name);
        animator.Play("Spin");
    }
}
