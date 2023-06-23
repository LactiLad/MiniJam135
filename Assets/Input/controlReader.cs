using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class controlReader : MonoBehaviour
{
    //refrences
    NewControls controls;
    simpleMovement sm;
    
    //vectors
    private void OnEnable() {
        controls.Player.Enable();
    }
    private void OnDisable() {
        controls.Player.Disable();
    }
    void Awake()
    {
        sm = GetComponent<simpleMovement>();

        controls = new NewControls();

        controls.Player.Down.performed += ctx => down();
        controls.Player.Up.performed += ctx => up();
        controls.Player.Right.performed += ctx => right();
        controls.Player.Left.performed += ctx => left();
    }

    void down() {
        if (sm.delay > sm.moveTime * (sm.moveStep-1))
            sm.down();
    }
    void up() {
        if (sm.delay > sm.moveTime * (sm.moveStep-1))
            sm.up();
    }
    void right() {
        if (sm.delay > sm.moveTime * (sm.moveStep-1))
            sm.right();
    }
    void left() {
        if (sm.delay > sm.moveTime * (sm.moveStep-1))
            sm.left();
    }
}