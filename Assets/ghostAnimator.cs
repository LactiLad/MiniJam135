using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostAnimator : MonoBehaviour
{
    Material mat;
    bool changeFrame = true;
    [SerializeField] Texture[] frames;
    int index = 0;

    // Update is called once per frame
    void Update()
    {
        if (changeFrame) {
            changeFrame = false;
            Invoke("ChangeFrame", 0.25f);
            GetComponent<Renderer>().material.SetTexture("_MainTex", frames[index]);
            if (index+1 < 3) index++;
            else index = 0;
        }
    }
    void ChangeFrame() {
        changeFrame = true;
    }
}
