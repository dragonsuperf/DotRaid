using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<MonoBehaviour>
{
    public GameObject[] chracters;
    public GameObject boss;
    public EffectManager effectManager;
    public Effect defaultBlastEffect;
    public Vector3 startPosition;
    public Vector3 bossRoomPosition;
    public DungeonCreator dungeonCreator;
    public Vector3 currentCameraPosition;

    private Point currentRoomKey;

    // Start is called before the first frame update
    protected override void Start()
    {
        setStartPoint();
        setCharactersAndEnemy();
        effectManager.AddEffectToPool("blast", defaultBlastEffect, 10);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject[] GetChars() => chracters;
    public GameObject GetBoss() => boss;


    private void setCharactersAndEnemy()
    {
        if(boss == null)
        {
            Debug.LogError("Boss is null.");
        }
        boss.transform.position = bossRoomPosition;
        boss.SetActive(false);

        foreach(GameObject ch in chracters)
        {
            GameObject heros = Instantiate(ch, startPosition, Quaternion.identity, this.gameObject.transform);
        }
        Camera.main.gameObject.transform.position = startPosition;
    }

    private void setStartPoint()
    {
        foreach (KeyValuePair<Point, DungeonRoom> room in dungeonCreator.Rooms)
        {
            if (room.Value.gameObject.name == "Origin Room")
            {
                startPosition = room.Value.gameObject.transform.position;
                currentRoomKey = room.Key;
                continue;
            }
            if (room.Value.gameObject.name == "Boss Room")
            {
                bossRoomPosition = room.Value.gameObject.transform.position;
                continue;
            }
        }
    }

    public void FindNextRoom()
    {
        Debug.Log(DungeonCreator.Instance.Rooms is null);

        foreach(KeyValuePair<Point, DungeonRoom> room in DungeonCreator._instance.Rooms)
        {
            foreach(Door door in room.Value.Doors)
            {
                if(door.DoorToWay == DoorDir.BossWay)
                {
                    Debug.Log(room.Value.ToString());
                    return;
                }
            }
        }
        return;
    }
    



}
