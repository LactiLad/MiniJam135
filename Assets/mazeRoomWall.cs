using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class mazeRoomWall : MonoBehaviour
{
    void OnEnable()
    {
        //transform.parent.GetComponent<mazeRoom>().enable(gameObject);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }
    void OnDisable()
    {
        //transform.parent.GetComponent<mazeRoom>().disable(gameObject);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
    public void forceDisable() {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}
