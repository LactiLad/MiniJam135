using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class maze : MonoBehaviour
{
    [SerializeField] public float roomWidth;
    [SerializeField] bool generate;
    [Range(1,50)]
    [ContextMenuItem("Reset Rooms (to load new prefab)", "resetRooms")]
    [SerializeField] public int height, width;
    [SerializeField] public GameObject mazeRoomPrefab;
    public List<List<GameObject>> mazeRooms = new List<List<GameObject>>();

    // Update is called once per frame
    void Update()
    {
        if (!generate) return;
        //Debug.Log(mazeRooms.Count + ", " + mazeRooms[0].Count);
        if (mazeRooms.Count != width || mazeRooms[0].Count != height) {
            if (mazeRooms.Count < width || mazeRooms[0].Count < height) {
                while (mazeRooms.Count < width) {//add width and initialize height
                    GameObject newRoom = Instantiate(mazeRoomPrefab, transform);
                    newRoom.transform.parent = transform;
                    List<GameObject> newList = new List<GameObject>();
                    newList.Add(newRoom);
                    mazeRooms.Add(newList); 
                }
                for (int i = 0; i < mazeRooms.Count; i++) {//fill out height
                    while (mazeRooms[i].Count < height) {
                        GameObject newRoom = Instantiate(mazeRoomPrefab, transform);
                        newRoom.transform.parent = transform;
                        mazeRooms[i].Add(newRoom);
                    }
                }
            } else {
                while (mazeRooms.Count > width) {//remove extra width
                    for (int i = mazeRooms[mazeRooms.Count-1].Count-1; i >= 0; i--) {
                        if (mazeRooms[mazeRooms.Count-1][i])
                            DestroyImmediate(mazeRooms[mazeRooms.Count-1][i]);
                    }
                    mazeRooms.Remove(mazeRooms[mazeRooms.Count-1]);
                }
                for (int i = 0; i < mazeRooms.Count; i++) {//remove extra height
                    while (mazeRooms[i].Count > height) {
                        if (mazeRooms[i][mazeRooms[i].Count - 1])
                            DestroyImmediate(mazeRooms[i][mazeRooms[i].Count - 1]);
                        mazeRooms[i].Remove(mazeRooms[i][mazeRooms[i].Count - 1]);
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
                        (roomWidth*j) - ((float)(mazeRooms[0].Count * roomWidth)/2f) + zoffset,
                        mazeRooms[i][j].transform.position.y, 
                        (roomWidth*i) - ((float)(mazeRooms.Count * roomWidth)/2f) + xoffset);
                }
            }
        }
    }
    public void removeWall(GameObject requester, GameObject wall) {
        if (!generate) return;
        for (int i = 0; i < mazeRooms.Count; i++) {//x
            for (int j = 0; j < mazeRooms[i].Count; j++) {//z
                if (mazeRooms[i][j].Equals(requester)) {
                    int[] neighbor = determineNeighbor(wall, i, j);
                    Debug.Log(i + ", " + j + " tried to remove " + neighbor[0] + ", " + neighbor[1]);
                    mazeRooms[neighbor[0]][neighbor[1]].GetComponent<mazeRoom>().removeWall(reverseWall(wall));
                }
            }
        }
    }
    int[] determineNeighbor(GameObject wall, int x, int z) {
        int[] result = new int[2];
        result[0] = x;
        result[1] = z;
        if (wall.tag.Equals("px")) result[1] = x+1;
        if (wall.tag.Equals("nx")) result[1] = x-1;
        if (wall.tag.Equals("pz")) result[0] = z+1;
        if (wall.tag.Equals("nz")) result[0] = z-1;
        return result;
    }
    string reverseWall(GameObject wall) {
        if (wall.tag.Equals("px")) return "nx";
        if (wall.tag.Equals("pz")) return "nz";
        if (wall.tag.Equals("nx")) return "px";
        return "pz";
    }
    void resetRooms() {
        if (!generate) return;
        while (mazeRooms.Count > 0) {//remove extra width
            for (int i = mazeRooms[mazeRooms.Count-1].Count-1; i >= 0; i--) {
                if (mazeRooms[mazeRooms.Count-1][i])
                    DestroyImmediate(mazeRooms[mazeRooms.Count-1][i]);
            }
            mazeRooms.Remove(mazeRooms[mazeRooms.Count-1]);
        }
        for (int i = 0; i < mazeRooms.Count; i++) {//remove extra height
            while (mazeRooms[i].Count > 0) {
                if (mazeRooms[i][mazeRooms[i].Count - 1])
                    DestroyImmediate(mazeRooms[i][mazeRooms[i].Count - 1]);
                mazeRooms[i].Remove(mazeRooms[i][mazeRooms[i].Count - 1]);
            }
        }
    }
}
