using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class mazeRoom : MonoBehaviour
{
    [SerializeField] public bool isTrap;
    public GameObject[] wallDirection = new GameObject[4];
    /*void Start()
    {
        wallDirection[0] = transform.Find("Wall-X").gameObject;
        wallDirection[1] = transform.Find("Wall+X").gameObject;
        wallDirection[2] = transform.Find("Wall-Z").gameObject;
        wallDirection[3] = transform.Find("Wall+Z").gameObject;
    }*/
    public void enable(GameObject wall) {
        //transform.parent.GetComponent<maze>().removeWall(gameObject, determineWall(name));
    }
    public void disable(GameObject wall) {
        transform.parent.GetComponent<maze>().removeWall(gameObject, wall);
    }
    public void removeWall(string tag) {
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).tag.Equals(tag))
                transform.GetChild(i).GetComponent<mazeRoomWall>().forceDisable();
        }
    }
}
