using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DungeonManager : Singleton<DungeonManager>
{
    public bool roomCleared = false;
    public Toggle clearToggle;
    public Point CurrPoint;
    public List<DungeonRoom> AdjancentRooms = new List<DungeonRoom>();
    public AStarManager AStarManager;

    protected override void Start()
    {
        clearToggle.onValueChanged.AddListener((bool val) =>
        {
            ClearToggleValueChanged(val);   
        });
    }

    public void SetOffClearToggle()
    {
        clearToggle.isOn = false;
    }

    public void ClearToggleValueChanged(bool val)
    {
        if (val)
        {
            roomCleared = true;
        }
        else
        {
            roomCleared = false;
        }
    }

    public DungeonRoom GetCurrentDungeonRoom(){
        return DungeonCreator.Instance.Rooms[CurrPoint];
    }

    public List<DungeonRoom> GetAdjacentRooms(Point currPoint){
        DungeonRoom currRoom = DungeonCreator.Instance.Rooms[currPoint];
        List<DungeonRoom> list = new List<DungeonRoom>();
        foreach(Door door in currRoom.Doors){
            if(door.IsOpen){
                list.Add(door.NextRoom);
            }
        }
        return list;
    }






}
