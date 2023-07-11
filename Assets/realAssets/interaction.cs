using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class interaction : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }
    public string type;
    public GameObject interactableObject;
}
