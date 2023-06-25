using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class controlRelay : MonoBehaviour
{
    //refrences
    Controls controls;
    simpleMovement sm;
    
    bool isDown, isUp, isRight, isLeft;
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

        controls = new Controls();

        controls.Player.Down.performed += ctx => isDown = true;//down();
        controls.Player.Down.canceled += ctx => isDown = false;//down();

        controls.Player.Up.performed += ctx => isUp = true;// up();
        controls.Player.Up.canceled += ctx => isUp = false;// up();

        controls.Player.Right.performed += ctx => isRight = true;// right();
        controls.Player.Right.canceled += ctx => isRight = false;// right();

        controls.Player.Left.performed += ctx => isLeft = true;// left();
        controls.Player.Left.canceled += ctx => isLeft = false;// left();

        controls.Player.Drink.performed += ctx => drink();
        controls.Player.Interact.performed += ctx => interact();
    }

    void Update()
    {
        if (isDown) down();
        if (isUp) up();
        if (isRight) right();
        if (isLeft) left();
    }

    void down() {
        sm.down();
    }
    void up() {
        sm.up();
    }
    void right() {
        sm.right();
    }
    void left() {
        sm.left();
    }
    void drink() {
        sm.drink();
    }
    void interact() {
        sm.interact();
    }
}