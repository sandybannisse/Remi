using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class AddCollider : MonoBehaviour
    {
        GameObject mesh;
    
        void Awake()
        {
            mesh = GameObject.Find ("Mesh");
            mesh.AddComponent<MeshCollider>();
        }
    
    }