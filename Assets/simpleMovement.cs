using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class simpleMovement : MonoBehaviour
{
    [SerializeField] public float moveStep = 4, moveTime = 0.5f, moveSpeed, winValSpeed;
    [SerializeField] LayerMask roomLayer, wallLayer;
    [SerializeField] GameObject potionMark, deathMark, text, spike;
    [SerializeField] Material cam;
    [SerializeField] Animator animator;
    [HideInInspector] public float delay = 0;
    [SerializeField] public bool potion = false, specialSpawn = false, blueKey = false, greenKey = false, yellowKey = false, purpleKey = false;
    float winVal;
    RaycastHit hit;
    Rigidbody rb;
    Transform spawnPoint;
    bool moving = false, turning = false, win = false, dead = true;
    //[HideInInspector] public bool interactable = false;
    [HideInInspector] public interaction interactableItem = null;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        live();
        spawnPoint = GameObject.Find("spawnPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (win) {
            winVal = Mathf.Lerp(winVal, 3.03f, Time.deltaTime * winValSpeed);
            cam.SetFloat("_WinVal", winVal);
            GameObject.Find("bottle").GetComponent<Image>().color = new Color(1, 1, 1, 1f-winVal*3f);
            GameObject.Find("interact").GetComponent<Image>().color = new Color(1, 1, 1, 1f-winVal*3f);
            GameObject.Find("blue key").GetComponent<Image>().color = new Color(1, 1, 1, 1f-winVal*3f);
            GameObject.Find("green key").GetComponent<Image>().color = new Color(1, 1, 1, 1f-winVal*3f);
            GameObject.Find("yellow key").GetComponent<Image>().color = new Color(1, 1, 1, 1f-winVal*3f);
            GameObject.Find("purple key").GetComponent<Image>().color = new Color(1, 1, 1, 1f-winVal*3f);
        }
        Vector3 roundPos = new Vector3(Mathf.Round(rb.position.x), rb.position.y, Mathf.Round(rb.position.z));
        if (rb.position != roundPos && !moving) {
            rb.MovePosition(roundPos);
        }
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 0.51f));
        delay += Time.deltaTime;
        GameObject.Find("interact").GetComponent<Image>().enabled = false;
        text.SetActive(false);
        if (interactableItem != null) {
            GameObject.Find("interact").GetComponent<Image>().enabled = true;
            if (interactableItem.type.Equals("trap") && !turning && !moving && !dead) {
                kill("trap");
                GetComponent<SFXHandler>().TrapSound();
                GameObject.Find("interact").GetComponent<Image>().enabled = false;
            } else if (interactableItem.type.Equals("trap 1")) {
                interactableItem.interactableObject.SetActive(true);
                GetComponent<SFXHandler>().DoorClose();
                GameObject.Find("interact").GetComponent<Image>().enabled = false;
            } else if (interactableItem.type.Equals("spooky guy")) {
                Destroy(interactableItem.interactableObject);
                GameObject.Find("interact").GetComponent<Image>().enabled = false;
            } else if (interactableItem.type.Equals("puzzle 3 D")) {
                interactableItem.interactableObject.SetActive(true);
                interactableItem.interactableObject0.SetActive(true);
                interactableItem.interactableObject1.SetActive(true);
                GetComponent<SFXHandler>().DoorClose();
                GameObject.Find("interact").GetComponent<Image>().enabled = false;
            } else if (interactableItem.type.Equals("puzzle 3 B")) {
                interactableItem.interactableObject.SetActive(false);
                Destroy(interactableItem.interactableObject0.GetComponent<interaction>());
                Destroy(interactableItem.interactableObject1.GetComponent<interaction>());
                GameObject.Find("interact").GetComponent<Image>().enabled = false;
            } else if (interactableItem.type.Equals("puzzle 5 space")) {
                specialSpawn = true;
                GameObject.Find("interact").GetComponent<Image>().enabled = false;
            }
            if (interactableItem.GetComponent<scroll>()) {
                text.SetActive(true);
                GetComponent<SFXHandler>().ScrollRead();
                GameObject.Find("interact").GetComponent<Image>().enabled = false;
                text.transform.Find("text").GetComponent<TextMeshProUGUI>().text = interactableItem.GetComponent<scroll>().text;
            }
        }
    }

    public void drink() {
        if (!potion) return;
        if (turning) return;
        if (moving) return;
        kill("potion");
    }
    public void interact() {
        if (turning) return;
        if (moving) return;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.51f, wallLayer)) {
            if (hit.rigidbody.transform.GetComponent<interaction>()) {
                interaction interactableObject = hit.rigidbody.transform.GetComponent<interaction>();
                if (interactableObject.type.IndexOf("door") >= 0) {
                    if (interactableObject.type.Equals("door")) {
                        Destroy(interactableObject.transform.gameObject);
                        GetComponent<SFXHandler>().DoorOpen();
                    } else if(interactableObject.type.Equals("starting door")) {
                        interactableObject.interactableObject.gameObject.SetActive(true);
                        GetComponent<SFXHandler>().DoorClose();
                        GetComponent<SFXHandler>().DoorOpen();
                        Destroy(interactableObject.transform.gameObject);
                    } else if (interactableObject.type.Equals("end door")) {
                        win = true;
                        GetComponent<SFXHandler>().DoorOpen();
                        GetComponent<SFXHandler>().Win();
                        Destroy(interactableObject.transform.gameObject);
                    } else if (interactableObject.type.Equals("spooky door")) {
                        GetComponent<SFXHandler>().DoorOpen();
                        Destroy(interactableObject.transform.gameObject);
                        Destroy(interactableObject.interactableObject.transform.gameObject);
                    } else if (interactableObject.type.Equals("cyan door")) {
                        if (purpleKey) {
                            GetComponent<SFXHandler>().DoorOpen();
                            Destroy(interactableObject.transform.gameObject);
                        } else {
                            GetComponent<SFXHandler>().LockedDoor();
                        }
                    } else if (interactableObject.type.Equals("blue door")) {
                        if (blueKey) {
                            GetComponent<SFXHandler>().DoorOpen();
                            Destroy(interactableObject.transform.gameObject);
                        } else {
                            GetComponent<SFXHandler>().LockedDoor();
                        }
                    } else if (interactableObject.type.Equals("yellow door")) {
                        if (yellowKey) {
                            GetComponent<SFXHandler>().DoorOpen();
                            Destroy(interactableObject.transform.gameObject);
                        } else {
                            GetComponent<SFXHandler>().LockedDoor();
                        }
                    } else if (interactableObject.type.Equals("green door")) {
                        if (greenKey) {
                            GetComponent<SFXHandler>().DoorOpen();
                            Destroy(interactableObject.transform.gameObject);
                        } else {
                            GetComponent<SFXHandler>().LockedDoor();
                        }
                    }
                } else if (interactableObject.type.IndexOf("button") >= 0) {
                    if (interactableObject.type.Equals("pink button")) {
                        GetComponent<SFXHandler>().DoorOpen();
                        GetComponent<SFXHandler>().ButtonPress();
                        interaction[] interactions = GameObject.FindObjectsByType<interaction>(FindObjectsSortMode.None);
                        foreach (interaction interaction in interactions) {
                            if (interaction.type.Equals("pink door")) {
                                Destroy(interaction.transform.gameObject);
                            }
                        }
                    } else if (interactableObject.type.Equals("purple button")) {
                        GetComponent<SFXHandler>().DoorOpen();
                        GetComponent<SFXHandler>().ButtonPress();
                        interaction[] interactions = GameObject.FindObjectsByType<interaction>(FindObjectsSortMode.None);
                        foreach (interaction interaction in interactions) {
                            if (interaction.type.Equals("purple door")) {
                                Destroy(interaction.transform.gameObject);
                            }
                        }
                    } else if (interactableObject.type.Equals("puzzle 3 button")) {
                        GetComponent<SFXHandler>().DoorOpen();
                        GetComponent<SFXHandler>().ButtonPress();
                        Debug.Log("puzzle 3 button");
                        interactableObject.interactableObject.SetActive(false);
                        interactableObject.interactableObject0.SetActive(false);
                    }
                }
            }
        }
        if (interactableItem != null) {
            Debug.Log(interactableItem.type);
            if (interactableItem.type.Equals("Concoction")) {
                Destroy(interactableItem.interactableObject);
                Destroy(interactableItem.interactableObject0);
                GetComponent<SFXHandler>().Get();
                potion = true;
                spawnPoint = GameObject.Find("spawnPoint").transform;
                interactableItem.GetComponent<BoxCollider>().enabled = false;
                GameObject.Find("bottle").GetComponent<Image>().enabled = true;
                Destroy(interactableItem);
            } else if (interactableItem.type.IndexOf("key") >= 0) {
                if (interactableItem.type.Equals("blue key")) {
                    blueKey = true;
                    GetComponent<SFXHandler>().Get();
                    GameObject.Find("blue key").GetComponent<Image>().enabled = true;;
                } else if (interactableItem.type.Equals("green key")) {
                    greenKey = true;
                    GetComponent<SFXHandler>().Get();
                    GameObject.Find("green key").GetComponent<Image>().enabled = true;
                } else if (interactableItem.type.Equals("yellow key")) {
                    yellowKey = true;
                    GetComponent<SFXHandler>().Get();
                    GameObject.Find("yellow key").GetComponent<Image>().enabled = true;
                } else if (interactableItem.type.Equals("cyan key")) {
                    purpleKey = true;
                    Destroy(interactableItem.interactableObject0);
                    specialSpawn = false;
                    GetComponent<SFXHandler>().Get();
                    GameObject.Find("purple key").GetComponent<Image>().enabled = true;
                }
                Destroy(interactableItem.interactableObject);
                interactableItem.GetComponent<BoxCollider>().enabled = false;
                Destroy(interactableItem);
            }
        }
    }
    public void kill(string type) {
        dead = true;
        Invoke("resetPos", 1f);
        if (type.Equals("potion")) {
            animator.Play("deathPotion", 0);
            GetComponent<SFXHandler>().PotionDrink();
            GameObject mark = Instantiate(potionMark, transform.position, Quaternion.identity);
            mark.transform.parent = null;
            Invoke("live", 1.25f);
        } else {//trap
            animator.Play("deathTrap", 0);
            GetComponent<SFXHandler>().TrapSound();
            GameObject mark = Instantiate(deathMark, transform.position, Quaternion.identity);
            spike.SetActive(true);
            mark.transform.parent = null;
            Invoke("live", 1.5f);
        }
    }
    public void live() {
        dead = false;
        animator.Play("relive", 0);
        spike.SetActive(false);
    }
    public void resetPos() {
        if (specialSpawn) {
            transform.position = GameObject.Find("puzzle5SpawnPoint").transform.position;
            transform.rotation = GameObject.Find("puzzle5SpawnPoint").transform.rotation;
        } else {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
    }
    void stopMove() {moving = false;}
    public void up() {
        if (turning || moving || dead) return;
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
        if (turning || moving || dead) return;
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
        if (turning || moving || dead) return;
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
        if (turning || moving || dead) return;
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
