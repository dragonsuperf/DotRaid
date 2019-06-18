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

    private Dictionary<DungeonRoom, List<Enemy>> enemiesGroup = new Dictionary<DungeonRoom, List<Enemy>>();
    public Dictionary<DungeonRoom, List<Enemy>> EnemiesGroup { get { return enemiesGroup; } }

    protected override void Start()
    {
        clearToggle.onValueChanged.AddListener((bool val) =>
        {
            ClearToggleValueChanged(val);
        });
    }

    public void KillAllEnemiesInThisRoom(){
        DungeonRoom thisRoom = GetCurrentDungeonRoom();

        for(int i = 0; i < enemiesGroup[thisRoom].Count; i++){
            enemiesGroup[thisRoom][i].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(enemiesGroup[thisRoom][i].DebugDead());
        }
    }


    // 던전 룸에 몬스터들을 배치합니다.
    public void SetEnemiesInRooms(DungeonRoom room, Enemy enemy){
        
        if(!enemiesGroup.ContainsKey(room)){
            enemiesGroup.Add(room, new List<Enemy>());
        }
        enemiesGroup[room].Add(enemy);
    }

    public void RemoveEnemyInRoom(DungeonRoom room, Enemy enemy){
        enemiesGroup[room].Remove(enemy);
        if(IsDungeonRoomClear(room)){
            roomCleared = true;
        }
    }

    public bool IsDungeonRoomClear(DungeonRoom room){
        if(enemiesGroup[room].Count == 0){
            return true;
        }
        else{
            return false;
        }
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
