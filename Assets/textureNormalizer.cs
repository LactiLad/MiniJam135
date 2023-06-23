using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class textureNormalizer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera cam;
    [ContextMenuItem("Update Resolution Or Aspect", "updateRes")]
    [Range(2, 1080)]
    [SerializeField] int resolution = 64;
    int resCheck;
    RawImage image;
    RenderTexture rt;
    int height = 64;
    int width = 64;
    Vector2 aspectRatio;
    float delay = 5f;
    // Update is called once per frame
    void Update()
    {
        if (cam) {
            if (!image) {
                image = GetComponent<RawImage>();
                cam.targetTexture = rt;
                rt = new RenderTexture(Screen.width,Screen.height,16,UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm);
            }
            delay += Time.deltaTime;
            if ((aspectRatio != new Vector2(Screen.height, Screen.width) || resCheck != resolution) && delay > 1f) {
                updateRes();
            }
        }
    }
    void updateRes() {
        resCheck = resolution;
        delay = 0f;
        if (Screen.height > Screen.width) {
            height = resolution;
            width = (int)(((float)Screen.height/(float)Screen.width) * resolution);
        } else {
            height = (int)(((float)Screen.height/(float)Screen.width) * resolution);
            width = resolution;
        }
        rt = new RenderTexture(width,height,16,UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm);
        rt.filterMode = FilterMode.Point;
        cam.targetTexture = rt;
        image.texture = rt;
        aspectRatio = new Vector2(Screen.height, Screen.width);
    }
}
