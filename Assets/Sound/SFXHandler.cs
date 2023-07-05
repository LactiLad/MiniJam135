using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXHandler : MonoBehaviour
{
    public static SFXHandler instance;
    public AudioSource Audio;
    public AudioSource Audio2;
    [SerializeField] AudioClip win, walk, turn, noWalk, doorOpen, doorClose, buttonPress, get, potionDrink, trapSound, deathSound, scrollRead, lockedDoor;


    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    public void Win() {
        Audio.clip = win;
        Audio.Play();
    }
    public void Get() {
        Audio.clip = get;
        Audio.Play();
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
    public void DoorOpen() {
        Audio.clip = doorOpen;
        Audio.Play();
    }
    public void DoorClose() {
        Audio.clip = doorClose;
        Audio.Play();
    }
    public void ButtonPress() {
        Audio.clip = buttonPress;
        Audio.Play();
    }
    public void PotionDrink() {
        Audio.clip = potionDrink;
        Audio.Play();
    }
    public void TrapSound() {
        Audio.clip = trapSound;
        Audio.Play();
        Audio2.clip = deathSound;
        Audio2.Play();
    }    
    public void ScrollRead() {
        Audio2.clip = scrollRead;
        Audio2.Play();
    }
    public void LockedDoor() {
        Audio.clip = lockedDoor;
        Audio.Play();
    }
}
