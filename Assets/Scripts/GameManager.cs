using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Character[] chracters;
    public List<Character> heroes = new List<Character>();
    private GameObject boss;

    public UIHelper uiHelper;
    
    public Vector3 startPosition;
    public Vector3 bossRoomPosition;
    public DungeonCreator dungeonCreator;
    public Vector3 currentCameraPosition;

    [HideInInspector]public GameObject EnemiesRoot;

    private Point currentRoomKey;
    private DungeonRoom currentRoom;

    public List<GameObject> Enemies = new List<GameObject>();
    public Stack<GameObject> EnemyStack = new Stack<GameObject>();


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        boss = Instantiate( Resources.Load("Prefabs/Enemy/Boss") as GameObject ) ;
        EnemiesRoot = new GameObject("EnemiesRoot");

        SkillManager.Instance.OnSet();
        EffectManager.Instance.OnSet();
        CharSelectManager.Instance.OnSet();
        
        SpawnEnemy(200);
        DungeonCreator.Instance.PlaceEnemies(8);
        setStartPoint();
        setCharactersAndEnemy();

        //bossRoomPosition = 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Character> GetChars() => heroes;
    public GameObject GetBoss() => boss;


    private void setCharactersAndEnemy()
    {
        if(boss == null)
        {
            Debug.LogError("Boss is null.");
        }
        boss.transform.position = startPosition;
        //boss.SetActive(false);

        int idx = 0;
        foreach(Character ch in chracters)
        {
            Character hero = Instantiate(ch.gameObject, startPosition, Quaternion.identity, this.gameObject.transform).GetComponent<Character>();
            hero.SetIDX(idx++);
            heroes.Add(hero);
        }
        Camera.main.gameObject.transform.position = new Vector3( startPosition.x, startPosition.y, Camera.main.gameObject.transform.position.z) ;
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

        // test code.
        //bossRoomPosition = startPosition;
    }
    
    public void MoveCameraToRoomPosition(DungeonRoom room)
    {
        Camera.main.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, -10);
    }

    private void SpawnEnemy(int count)
    {
        // Debug.Log("EnemieLength: " + Enemies.Count);
        if (Enemies.Count == 0) return;
        for(int i = 0; i < count; i++)
        {
            int num = Random.Range(0, Enemies.Count);
            GameObject enemy = Instantiate(Enemies[num]);
            enemy.transform.position = this.transform.position;
            enemy.transform.parent = this.transform;
            EnemyStack.Push(enemy);
            enemy.SetActive(false);
        }
    }


}
