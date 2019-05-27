using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text;


public enum DoorDir { None, StartWay, BossWay };
public enum Direction { None, Up, Bottom, Right, Left };



public class DungeonRoom : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TilemapRenderer tilemapRend;

    // real door
    public Door[] Doors = new Door[4];

    // old door
    public bool UpperDoor { get; set; }
    public bool BottomDoor { get; set; }
    public bool LeftDoor { get; set; }
    public bool RightDoor { get; set; }

    public List<DoorDir> DoorDirs = new List<DoorDir>();
    public DoorDir UpperDoorWay;
    public DoorDir BottomDoorWay;
    public DoorDir RightDoorWay;
    public DoorDir LeftDoorWay;

    public Point RoomCoord { get; private set; }
    public DungeonRoom NextRoom;
    public DungeonRoom PrevRoom;

    [SerializeField] private GameObject[] DoorObjects = new GameObject[4];

    [SerializeField] private GameObject UpperDoorObj;
    [SerializeField] private GameObject RightDoorObj;
    [SerializeField] private GameObject BottomDoorObj;
    [SerializeField] private GameObject LeftDoorObj;

    public GameObject GetDoorObj(int num)
    {
        switch (num)
        {
            case 0: return UpperDoorObj;
            case 1: return RightDoorObj;
            case 2: return BottomDoorObj;
            case 3: return LeftDoorObj;
        }
        return null;
    }

    private void Awake()
    {
        DoorDirs.Add(UpperDoorWay);
        DoorDirs.Add(BottomDoorWay);
        DoorDirs.Add(LeftDoorWay);
        DoorDirs.Add(RightDoorWay);

        for(int i = 0; i < 4; i++)
        {
            Doors[i] = new Door(this);
        }
    }

    public void setDoorProperties(int number, bool isOpen, DoorDir doorDir)
    {
        DoorObjects[number].SetActive(isOpen);
        Doors[number].SetDoorProperties(number, isOpen, doorDir);
        Doors[number].DoorObject = GetDoorObj(number);
        Doors[number].DoorObject.GetComponent<DoorControl>().Door = Doors[number];
        Doors[number].DoorObject.GetComponent<DoorControl>().doorWay = number;
        // Debug.Log(Doors[number].DoorObject.GetComponent<DoorControl>().Door.ToString());
    }

    public void SetDoorDir(int dir, DoorDir way)
    {
        switch (dir)
        {
            case 0: UpperDoorWay = way; break;
            case 1: RightDoorWay = way; break;
            case 2: BottomDoorWay = way; break;
            case 3: LeftDoorWay = way; break;
        }
    }

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

    public override string ToString(){
        return "Current Room " + RoomCoord.ToString();
    }
}

public class Door
{
    public DungeonRoom Parent { get; private set; }
    public bool IsOpen { get; private set; }
    public DoorDir DoorToWay { get; private set; }
    public Direction DoorDirection { get; private set; }
    public DungeonRoom PrevRoom { get; private set; }
    public DungeonRoom NextRoom { get; private set; }
    public GameObject DoorObject;

    public void SetNextRoom(DungeonRoom room)
    {
        NextRoom = room;
        Parent.NextRoom = room;
    }

    public void SetPrevRoom(DungeonRoom room)
    {
        PrevRoom = room;
        Parent.PrevRoom = room;
    }

    public Door(DungeonRoom parent)
    {
        Parent = parent;
        IsOpen = false;
        DoorToWay = DoorDir.None;
        DoorDirection = Direction.None;
        PrevRoom = null;
        NextRoom = null;
    }

    public void SetDoorProperties(int number, bool isOpen, DoorDir doorDir)
    {
        IsOpen = isOpen;
        DoorToWay = doorDir;
        DoorDirection = Door.GetDirectionFromNumber(number);

    }

    public static Direction GetDirectionFromNumber(int number)
    {
        switch (number)
        {
            case 0: return Direction.Up;
            case 1: return Direction.Right;
            case 2: return Direction.Bottom;
            case 3: return Direction.Left;
        }
        return Direction.None;
    }

    private static int OppositeDoorNumber(int way)
    {
        switch (way)
        {
            case 0: return 2;
            case 1: return 3;
            case 2: return 0;
            case 3: return 1;
        }
        return -1;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Room: " + Parent.name);
        sb.Append("\nDoorDirection: " + DoorDirection);
        return sb.ToString();
    }
}
