using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maze : MonoBehaviour
{
    [SerializeField] public float roomWidth;
    [Range(0,50)]
    [SerializeField] public int width, height;
    [SerializeField] public GameObject mazeRoomPrefab;
    public List<List<GameObject>> mazeRooms = new List<List<GameObject>>();

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(mazeRooms.Count + ", " + mazeRooms[0].Count);
        if (mazeRooms.Count != width || mazeRooms[0].Count != height) {
            if (mazeRooms.Count < width || mazeRooms[0].Count < height) {
                while (mazeRooms.Count < width) {
                    GameObject newRoom = Instantiate(mazeRoomPrefab, transform);
                    newRoom.transform.parent = transform;
                    List<GameObject> newList = new List<GameObject>();
                    newList.Add(newRoom);
                    mazeRooms.Add(newList); 
                }
                for (int i = 0; i < mazeRooms.Count; i++) {
                    while (mazeRooms[i].Count < height) {
                        GameObject newRoom = Instantiate(mazeRoomPrefab, transform);
                        newRoom.transform.parent = transform;
                        mazeRooms[i].Add(newRoom);
                    }
                }
            }
            float xoffset = 0;
            float zoffset = 0;
            if (mazeRooms.Count %2 != 0) xoffset = (roomWidth/2f);
            if (mazeRooms[0].Count %2 != 0) zoffset = (roomWidth/2f);
            for (int i = 0; i < mazeRooms.Count; i++) {
                for (int j = 0; j < mazeRooms[i].Count; j++) {
                    mazeRooms[i][j].transform.localPosition = new Vector3(
                        (roomWidth*i) - ((float)(mazeRooms.Count * roomWidth)/2f) + xoffset,
                        mazeRooms[i][j].transform.position.y, 
                        (roomWidth*j) - ((float)(mazeRooms[0].Count * roomWidth)/2f) + zoffset);
                }
            }
        }
    }
}
