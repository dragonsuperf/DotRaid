using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Character[] chracters;
    public List<Character> heroes = new List<Character>();
    private Boss boss = new Boss();
    [SerializeField]
    private GameObject mapGrid;

    public UIHelper uiHelper;
    
    public Vector3 startPosition;
    public Vector3 bossRoomPosition;
    public DungeonCreator dungeonCreator;
    public Vector3 currentCameraPosition;

    [HideInInspector]public GameObject EnemiesRoot;

    private Point currentRoomKey;
    private DungeonRoom currentRoom;

    public List<Enemy> Enemies = new List<Enemy>();
    public Stack<Enemy> EnemyStack = new Stack<Enemy>();

    public List<Enemy> EnemyList = new List<Enemy>();
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
        EnemiesRoot = new GameObject("EnemiesRoot");
        dungeonCreator = DungeonCreator.Instance;

        //AStarManager.Instance.OnSet();
        DungeonManager.Instance.OnSet();
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
    public Enemy GetBoss() => boss;
    public GameObject GetMapGrid() => mapGrid;


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
        int bossIndex = 0; //임시처리
        var bosPrefab = Resources.Load("Prefabs/Enemy/Boss") as GameObject;
        boss = Instantiate(bosPrefab, EnemiesRoot.transform).GetComponent<Boss>();
        boss.SetIDX(bossIndex++);
        EnemyStack.Push(boss);
        EnemyList.Add(boss);

        // Debug.Log("EnemieLength: " + Enemies.Count);
        if (Enemies.Count == 0) return;
        for(int i = bossIndex; i < count; i++)
        {
            int num = Random.Range(0, Enemies.Count);
            Enemy enemy = Instantiate(Enemies[num], EnemiesRoot.transform) as Enemy;
            enemy.SetIDX(i);
            enemy.transform.position = this.transform.position;
            enemy.transform.parent = this.transform;
            EnemyStack.Push(enemy);
            EnemyList.Add(enemy);
            enemy.gameObject.SetActive(false);
        }
    }


}
