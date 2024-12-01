using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AudioSource footstepsSound, sprintSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (!sprintSound.isPlaying)
                {
                    sprintSound.Play();
                    footstepsSound.Stop();
                }
            }
            else
            {
                if (!footstepsSound.isPlaying)
                {
                    footstepsSound.Play();
                    sprintSound.Stop();
                }
            }
        }
        else
        {
            footstepsSound.Stop();
            sprintSound.Stop();
        }
    }
}
