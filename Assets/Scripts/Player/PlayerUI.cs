using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Updatetext(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}
