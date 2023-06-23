using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFlicker : MonoBehaviour
{

    public Light newLight;
    float startingIntensity;
    public float flickerTime;
    float timeSinceLast = 0;
    void Start()
    {
        newLight = GetComponent<Light>();
        startingIntensity = newLight.range;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLast += Time.deltaTime;
        if (timeSinceLast > flickerTime) {
            timeSinceLast = 0;
            newLight.range = (-1*Mathf.Pow(Random.value/2,4)+1)*startingIntensity;
        }
    }
}
