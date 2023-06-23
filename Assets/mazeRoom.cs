using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class mazeRoom : MonoBehaviour
{
    [HideInInspector] public GameObject wallnx, wallpx, wallnz, wallpz;
    public bool iswallnx, iswallpx, iswallnz, iswallpz;
    public void enable(string name) {
        if (name.IndexOf("-") > 0) {
            if (name.IndexOf("X") > 0) {
                iswallnx = true;
            } else {
                iswallnz = true;
            }
        } else {
            if (name.IndexOf("X") > 0) {
                iswallpx = true;
            } else {
                iswallpz = true;
            }
        }
    }
    public void disable(string name) {
        if (name.IndexOf("-") > 0) {
            if (name.IndexOf("X") > 0) {
                iswallnx = false;
            } else {
                iswallnz = false;
            }
        } else {
            if (name.IndexOf("X") > 0) {
                iswallpx = false;
            } else {
                iswallpz = false;
            }
        }
    }
}
