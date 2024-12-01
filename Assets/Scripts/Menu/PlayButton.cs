using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private void OnMouseUpAsButton(){
        Debug.Log("Play Button was pressed!");
        SceneManager.LoadScene("SampleScene");
        Debug.Log("Load Main Game Screen.");
    }
}
