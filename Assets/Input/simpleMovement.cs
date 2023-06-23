using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMovement : MonoBehaviour
{
    [SerializeField] public float moveStep = 4, moveTime = 0.5f;
    [SerializeField] LayerMask layer;
    [HideInInspector] public float delay = 0;
    RaycastHit hit;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;
    }

    public void up() {
        if (!Physics.Raycast(transform.position, transform.forward, out hit, 1f, layer)) {
            delay = 0f;
            for (int i = 0; i < moveStep; i++)
            {
                Invoke("mup", i*moveTime);
            }
        } else {
            GetComponent<SFXHandler>().NoWalk();
        }
    }
    void mup() {
        GetComponent<SFXHandler>().Walk();
        transform.position = transform.position + (transform.forward*(1f/moveStep));
    }
    public void down() {
        if (!Physics.Raycast(transform.position, -transform.forward, out hit, 1f, layer)) {
            delay = 0f;
            for (int i = 0; i < moveStep; i++)
            {
                Invoke("mdown", i*moveTime);
            }
        } else {
            GetComponent<SFXHandler>().NoWalk();
        }
    }
    void mdown() {
        GetComponent<SFXHandler>().Walk();
        transform.position = transform.position - (transform.forward*(1f/moveStep));
    }
    public void right() {
        delay = 0f;
        for (int i = 0; i < moveStep; i++)
        {
            Invoke("mright", i*moveTime);
        }
    }
    void mright() {
        GetComponent<SFXHandler>().Turn();
        transform.rotation = Quaternion.AngleAxis(90/moveStep, transform.up)*transform.rotation;
    }
    public void left() {
        delay = 0f;
        for (int i = 0; i < moveStep; i++)
        {
            Invoke("mleft", i*moveTime);
        }    
    }
    void mleft() {
        GetComponent<SFXHandler>().Turn();
        transform.rotation = Quaternion.AngleAxis(-90/moveStep, transform.up)*transform.rotation;
    }
}
