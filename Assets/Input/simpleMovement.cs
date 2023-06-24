using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMovement : MonoBehaviour
{
    [SerializeField] public float moveStep = 4, moveTime = 0.5f, moveSpeed;
    [SerializeField] LayerMask roomLayer, wallLayer;
    [HideInInspector] public float delay = 0;
    RaycastHit hit;
    Rigidbody rb;
    bool killDelay = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 5f));
        delay += Time.deltaTime;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.125f, roomLayer)) {
            Debug.Log("room");
            kill();
        }
    }

    public void kill() {
        transform.position = GameObject.Find("spawnPoint").transform.position;
        transform.rotation = GameObject.Find("spawnPoint").transform.rotation;
        killDelay = true;
        Invoke("resetKillDelay", moveTime*(moveStep-1));
    }
    void resetKillDelay() {killDelay = false;}
    public void up() {
        if (!Physics.Raycast(transform.position, transform.forward, out hit, 5f, wallLayer) && !killDelay) {
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
        if (killDelay) return;
        GetComponent<SFXHandler>().Walk();
        transform.position = transform.position + (transform.forward*(1f/moveStep)*moveSpeed);
    }
    public void down() {
        if (!Physics.Raycast(transform.position, -transform.forward, out hit, 5f, wallLayer) && !killDelay) {
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
        if (killDelay) return;
        GetComponent<SFXHandler>().Walk();
        transform.position = transform.position - (transform.forward*(1f/moveStep)*moveSpeed);
    }
    public void right() {
        if (killDelay) return;
        delay = 0f;
        for (int i = 0; i < moveStep; i++)
        {
            Invoke("mright", i*moveTime);
        }
    }
    void mright() {
        if (killDelay) return;
        GetComponent<SFXHandler>().Turn();
        transform.rotation = Quaternion.AngleAxis(90/moveStep, transform.up)*transform.rotation;
    }
    public void left() {
        if (killDelay) return;
        delay = 0f;
        for (int i = 0; i < moveStep; i++)
        {
            Invoke("mleft", i*moveTime);
        }    
    }
    void mleft() {
        if (killDelay) return;
        GetComponent<SFXHandler>().Turn();
        transform.rotation = Quaternion.AngleAxis(-90/moveStep, transform.up)*transform.rotation;
    }
    void OnTriggerEnter(Collider other) {
        if (other.transform.tag.Equals("room")) {
            if (other.GetComponent<mazeRoom>().isTrap) {
                kill();
            }
        }
    }
}
