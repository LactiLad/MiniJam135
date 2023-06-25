using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class simpleMovement : MonoBehaviour
{
    [SerializeField] public float moveStep = 4, moveTime = 0.5f, moveSpeed, winValSpeed;
    [SerializeField] LayerMask roomLayer, wallLayer;
    [SerializeField] Material cam;
    [HideInInspector] public float delay = 0;
    float winVal;
    RaycastHit hit;
    Rigidbody rb;
    Transform spawnPoint;
    bool moving = false, turning = false, win = false;
    //[HideInInspector] public bool interactable = false;
    [HideInInspector] public interaction interactableItem = null;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnPoint = GameObject.Find("tutorialSpawnPoint").transform;
        kill();
    }

    // Update is called once per frame
    void Update()
    {
        if (win) {
            winVal = Mathf.Lerp(winVal, 3.03f, Time.deltaTime * winValSpeed);
            cam.SetFloat("_WinVal", winVal);
        }
        Vector3 roundPos = new Vector3(Mathf.Round(rb.position.x), rb.position.y, Mathf.Round(rb.position.z));
        if (rb.position != roundPos && !moving) {
            rb.MovePosition(roundPos);
        }
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 0.51f));
        delay += Time.deltaTime;
        if (interactableItem != null) {
            if (interactableItem.type.Equals("trap")) {
                kill();
            }
        }
    }

    public void drink() {
        if (turning) return;
        if (moving) return;
        kill();
    }
    public void interact() {
        if (turning) return;
        if (moving) return;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.51f, wallLayer)) {
            if (hit.rigidbody.transform.GetComponent<interaction>()) {
                interaction interactableObject = hit.rigidbody.transform.GetComponent<interaction>();
                if (interactableObject.type.Equals("door")) {
                    Destroy(interactableObject.transform.gameObject);
                } if (interactableObject.type.Equals("end door")) {
                    win = true;
                    Destroy(interactableObject.transform.gameObject);

                }
            }
        }
        if (interactableItem != null) {
            Debug.Log(interactableItem.type);
            if (interactableItem.type.Equals("Concoction")) {
                Destroy(interactableItem.interactableObject);
                interactableItem.GetComponent<BoxCollider>().enabled = false;
                GameObject.Find("bottle").GetComponent<Image>().enabled = true;
                spawnPoint = GameObject.Find("spawnPoint").transform;
                Destroy(interactableItem);
            }
        }
    }
    public void kill() {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }
    void stopMove() {moving = false;}
    public void up() {
        if (turning || moving) return;
        if (!Physics.Raycast(transform.position, transform.forward, out hit, 0.51f, wallLayer)) {
            moving = true;
            for (int i = 0; i < moveStep; i++) {
                Invoke("mup", (moveTime/moveStep)*i);
            }
            Invoke("stopMove", moveTime);
        } else {
            GetComponent<SFXHandler>().NoWalk();
        }
    }
    void mup() {
        if (!Physics.Raycast(transform.position, transform.forward, out hit, 0.51f, wallLayer)) {
            GetComponent<SFXHandler>().Walk();
            rb.MovePosition(rb.position + rb.transform.forward*(1f/moveStep)*moveSpeed);
        }
    }
    public void down() {
        if (turning || moving) return;
        if (!Physics.Raycast(transform.position, -transform.forward, out hit, 0.51f, wallLayer)) {
            moving = true;
            for (int i = 0; i < moveStep; i++) {
                Invoke("mdown", (moveTime/moveStep)*i);
            }
            Invoke("stopMove", moveTime);
        } else {
            GetComponent<SFXHandler>().NoWalk();
        }
    }
    void mdown() {
        if (!Physics.Raycast(transform.position, -transform.forward, out hit, 0.51f, wallLayer)) {
            GetComponent<SFXHandler>().Walk();
            rb.MovePosition(rb.position - rb.transform.forward*(1/moveStep)*moveSpeed);
        }
    }
    void stopTurn() {turning = false;}
    public void right() {
        if (turning || moving) return;
        turning = true;
        for (int i = 0; i < moveStep; i++) {
            Invoke("mright", (moveTime/moveStep)*i);
        }
        Invoke("stopTurn", moveTime);
    }
    void mright() {
        GetComponent<SFXHandler>().Turn();
        rb.MoveRotation(Quaternion.AngleAxis(90/moveStep, transform.up)*rb.rotation);
    }
    public void left() {
        if (turning || moving) return;
        turning = true;
        for (int i = 0; i < moveStep; i++) {
            Invoke("mleft", (moveTime/moveStep)*i);
        }
        Invoke("stopTurn", moveTime);
    }
    void mleft() {
        GetComponent<SFXHandler>().Turn();
        rb.MoveRotation(Quaternion.AngleAxis(-90/moveStep, transform.up)*rb.rotation);
    }
    void OnTriggerEnter(Collider other) {
        if (other.transform.tag.Equals("room")) {
            if (other.GetComponent<interaction>()) {
                interactableItem = other.GetComponent<interaction>();
            }
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.transform.tag.Equals("room")) {
            if (other.GetComponent<interaction>()) {
                interactableItem = null;
            }
        }
    }
}
