using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class pillar : MonoBehaviour
{
    void Update() {
        Vector3 roundPos = new Vector3(Mathf.Round(transform.position.x*2f),Mathf.Round(transform.position.y*2f),Mathf.Round(transform.position.z*2f))/2f;
        if (transform.position != roundPos) {
            transform.position = roundPos;
        }
    }
}
