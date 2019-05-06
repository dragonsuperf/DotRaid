using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Character[] chracters;
    public List<GameObject> heroes = new List<GameObject>();
    public GameObject boss;
    public EffectManager effectManager;
    public Effect defaultBlastEffect;
    public Vector3 startPosition;
    public Vector3 bossRoomPosition;
    public DungeonCreator dungeonCreator;
    public Vector3 currentCameraPosition;

    private Point currentRoomKey;
    private DungeonRoom currentRoom;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        setStartPoint();
        setCharactersAndEnemy();
        effectManager.AddEffectToPool("blast", defaultBlastEffect, 10);
        SkillManager.Instance.OnSet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Character[] GetChars() => chracters;
    public GameObject GetBoss() => boss;


    private void setCharactersAndEnemy()
    {
        if(boss == null)
        {
            Debug.LogError("Boss is null.");
        }
        boss.transform.position = bossRoomPosition;
        boss.SetActive(false);

        foreach(Character ch in chracters)
        {
            GameObject hero = Instantiate(ch.gameObject, startPosition, Quaternion.identity, this.gameObject.transform);
            heroes.Add(hero);
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
                currentRoom = DungeonCreator.Instance.Rooms[currentRoomKey];
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
        if (currentRoom.NextRoom == null) return;

        DungeonRoom nextRoom = currentRoom.NextRoom;

        Vector3 move = currentRoom.transform.position - nextRoom.transform.position;
        foreach(GameObject ch in heroes)
        {
            ch.gameObject.transform.position -= move;
        }
        Camera.main.transform.position = nextRoom.transform.position;

        currentRoom = nextRoom;
        currentRoomKey = currentRoom.RoomCoord;
    }
    
    //public void MoveCharacters()
    //{
    //    Vector3 move = new Vector3(20f, 20f, 0f);
    //    foreach (GameObject ch in chracters)
    //    {
    //        Debug.Log(ch.name);
    //        Debug.Log(ch.gameObject.transform.position);
    //        ch.gameObject.transform.position += move;
    //        Debug.Log(ch.gameObject.transform.position);
    //    }
    //}


}
