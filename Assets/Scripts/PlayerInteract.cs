using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{

    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask  mask;
    private PlayerUI playerUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent <PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.Updatetext(string.Empty); // so the text is empty if not there
        //Ray is at the center of the camera, looking forward
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, distance,mask)) // if we hit something, then
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                playerUI.Updatetext(hitInfo.collider.GetComponent<Collider>().GetComponent<Interactable>().promptMessage);
            }
        }

    }
}
