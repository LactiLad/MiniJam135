using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboarder : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation*Quaternion.Euler(90,-180,0);
        transform.localPosition = new Vector3(Camera.main.transform.forward.x*0.499f, transform.localPosition.y, Camera.main.transform.forward.z*0.499f);
    }
}
