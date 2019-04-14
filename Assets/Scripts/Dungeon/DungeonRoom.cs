using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class DungeonRoom : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TilemapRenderer tilemapRend;
    public bool UpperDoor { get; set; }
    public bool BottomDoor { get; set; }
    public bool LeftDoor { get; set; }
    public bool RightDoor { get; set; }


    public Point RoomCoord { get; private set; }

    [SerializeField] private GameObject UpperDoorObj;
    [SerializeField] private GameObject RightDoorObj;
    [SerializeField] private GameObject BottomDoorObj;
    [SerializeField] private GameObject LeftDoorObj;

    public void DoorOpen(int dir)
    {
        switch (dir)
        {
            case 0: UpperDoor = true;   UpperDoorObj.SetActive(true);    break;
            case 1: RightDoor = true;   RightDoorObj.SetActive(true);    break;
            case 2: BottomDoor = true;  BottomDoorObj.SetActive(true);   break;
            case 3: LeftDoor = true;    LeftDoorObj.SetActive(true);     break;
        }
    }

    public void DoorClose(int dir)
    {
        switch (dir)
        {
            case 0: UpperDoor = true; UpperDoorObj.SetActive(false); break;
            case 1: RightDoor = true; RightDoorObj.SetActive(false); break;
            case 2: BottomDoor = true; BottomDoorObj.SetActive(false); break;
            case 3: LeftDoor = true; LeftDoorObj.SetActive(false); break;
        }
    }

    public void SetRoomCoord(int x, int y)
    {
        if(RoomCoord == null)
        {
            RoomCoord = new Point(x, y);
            return;
        }

        RoomCoord.X = x;
        RoomCoord.Y = y;
    }
}
