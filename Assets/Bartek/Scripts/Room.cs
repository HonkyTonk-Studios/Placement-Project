using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    StartRoom,
    BossRoom,
    EnemyRoom,
    EmptyRoom,
    TreasureRoom,
}

public class Room : MonoBehaviour
{
    public List<Hallway> hallways = new List<Hallway>();

    //public int roomType;
    public RoomType roomType;

    //floor object temporarily used for color changing to know what roomtype it is
    public GameObject floor;

    public bool inRoomTrigger = false, roomCleared = true;

    //bools to check if there is a hallway in the corresponding position
    public bool topHallway, rightHallway, bottomHallway, leftHallway;

    private void Start()
    {
        //default room to be empty to avoid errors if roomType somehow does not get assigned
        roomType = RoomType.EmptyRoom;
    }

    //loops through children and assigns floor object if it finds an object named Floor
    public void GetFloor()
    {
        foreach (Transform t in transform)
        {
            if (t.gameObject.name == "Floor")
            {
                floor = t.gameObject;
                return;
            }
        }
    }

    public void GetHallways()
    {
        hallways.Clear();

        //loops through all children and add the ones that have a hallway script to hallway array
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Hallway>() != null)
                hallways.Add(child.gameObject.GetComponent<Hallway>());
        }

        foreach (Hallway hallway in hallways)
        {
            //checks if there is a hallway on the top side
            if (hallway.transform.position.z > transform.position.z)
            {
                hallway.direction = 1;
                topHallway = true;
            }

            //checks if there is a hallway on the right side
            if (hallway.transform.position.x > transform.position.x)
            {
                hallway.direction = 2;
                rightHallway = true;
            }

            //checks if there is a hallway on the bottom side
            if (hallway.transform.position.z < transform.position.z)
            {
                hallway.direction = 3;
                bottomHallway = true;
            }

            //checks if there is a hallway on the left side
            if (hallway.transform.position.x < transform.position.x)
            {
                hallway.direction = 4;
                leftHallway = true;
            }
        }
    }

    public void SetRoomByType()
    {
        switch (roomType)
        {
            case RoomType.StartRoom:
                SetRoomColour(Color.green);
                roomCleared = true;
                break;
            case RoomType.BossRoom:
                SetRoomColour(Color.red);
                roomCleared = false;
                break;
            case RoomType.EnemyRoom:
                SetRoomColour(Color.grey);
                roomCleared = false;
                break;
            case RoomType.EmptyRoom:
                SetRoomColour(Color.white);
                roomCleared = true;
                break;
            case RoomType.TreasureRoom:
                SetRoomColour(Color.yellow);
                roomCleared = true;
                break;
            default:
                roomCleared = true;
                break;
        }
    }
    
    void SetRoomColour(Color colour)
    {
        floor.GetComponent<MeshRenderer>().material.color = colour;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRoomTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRoomTrigger = false;
        }
    }

    public void OpenDoors()
    {
        foreach (Hallway hallway in hallways)
        {
            hallway.OpenDoor();
        }
    }

    public void CloseDoors()
    {
        foreach (Hallway hallway in hallways)
        {
            hallway.CloseDoor();
        }
    }

    public void StartRoom()
    {
        CloseDoors();
        //start whatever this room does based on type of room.
        Invoke("OpenDoors", 5);
        roomCleared = true;
    }
}
