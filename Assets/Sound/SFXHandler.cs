using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXHandler : MonoBehaviour
{
    public static SFXHandler instance;
    public AudioSource Audio;
    public AudioClip walk;
    public AudioClip turn;
    public AudioClip noWalk;


    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    public void Walk() {
        Audio.clip = walk;
        Audio.Play();
    }
    public void Turn() {
        Audio.clip = turn;
        Audio.Play();
    }
    public void NoWalk() {
        Audio.clip = noWalk;
        Audio.Play();
    }
}
