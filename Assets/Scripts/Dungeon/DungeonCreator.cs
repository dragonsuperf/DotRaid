using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonCreator : Singleton<DungeonCreator>
{
    [SerializeField] private DungeonRoom roomPrefab;
    private float mapWidth, mapHeight;
    private Stack<DungeonRoom> roomStack = new Stack<DungeonRoom>();
    private bool[,] mapArray;
    private int beforeCoorX, beforeCoorY;
    private int coorX, coorY;

    public GameObject[] InnerPattern = new GameObject[5];


    private Dictionary<Point, DungeonRoom> roomDict = new Dictionary<Point, DungeonRoom>();
    public Dictionary<Point, DungeonRoom> Rooms
    {
        get { return roomDict; }
    }
    private int roomNumber = 0;
    private Point CurrPoint, PrevPoint;
    
    protected override void Awake()
    {
        InitRooms();
        //int depth = 10;
        //int level = 0;
        Point origin = new Point(10, 10);
        //CreateDungeonRooms(ref level, ref depth, origin, Point.GetRandomWay(origin), true);

        

        CreateRooms(10, origin, null, -1, -1, true);
    }

    protected override void Start()
    {
        base.Start();
        PlaceEnemies(8);
    }

    private void PlaceEnemies(int enemyCount)
    {
        foreach(KeyValuePair<Point, DungeonRoom> room in roomDict)
        {
            if(room.Value.gameObject.name == "Origin Room")
            {
                continue;
            }
            SpawnPoint spPoint = room.Value.GetComponentInChildren<SpawnPoint>();
            GameObject[] enemies = new GameObject[enemyCount];
            for(int i = 0; i < enemyCount; i++)
            {
                if (GameManager.Instance.EnemyStack.Count == 0) break;
                enemies[i] = GameManager.Instance.EnemyStack.Pop();
                enemies[i].SetActive(true);
                enemies[i].transform.position = spPoint.spawnPoint[Random.Range(0, spPoint.spawnPoint.Length)].position;
            }
        }
    }

    //private void CreateToWayDirection(int globalDirection, int depth, DungeonRoom from)
    //{
    //    if (depth <= 0) return;

    //    while (true)
    //    {
    //        Point createdPt = Point.WayToPoint(globalDirection);
    //        if (!mapArray[CurrPoint.X + createdPt.X, CurrPoint.Y + createdPt.Y])
    //        {
    //            mapArray[CurrPoint.X + createdPt.X, CurrPoint.Y + createdPt.Y] = true;
    //            break;
    //        }
    //        else
    //        {
    //            continue;
    //        }
    //    }


    //    depth--;
    //    CreateToWayDirection(globalDirection, depth, from);
    //}

    //private void CreateToWay(int way, int depth)
    //{
    //    if (depth <= 0) return;

    //    int[] wayArray = new int[] { };
    //    switch (way)
    //    {
    //        case 0:
    //            wayArray = new int[] { 1, 2, 3 };
    //            break;
    //        case 1:
    //            wayArray = new int[] { 0, 2, 3 };
    //            break;
    //        case 2:
    //            wayArray = new int[] { 0, 1, 3 };
    //            break;
    //        case 3:
    //            wayArray = new int[] { 0, 1, 2 };
    //            break;
    //    }
    //    // way 방향의 랜덤 point를 구함.
    //    Point createDir = null;
    //    int num;
    //    while (true)
    //    {   
    //        num = wayArray[Random.Range(0, 3)];
    //        num = OppositeDoorNumber(num);
    //        createDir = Point.WayToPoint(num);
    //        if (!mapArray[CurrPoint.X + createDir.X, CurrPoint.X + createDir.Y]) break;

    //        if (mapArray[CurrPoint.X + 0, CurrPoint.X + 1] &&
    //            mapArray[CurrPoint.X + 1, CurrPoint.X + 0] &&
    //            mapArray[CurrPoint.X + 0, CurrPoint.X - 1] &&
    //            mapArray[CurrPoint.X - 1, CurrPoint.X + 0])
    //        {
    //            print("all blocks");
    //            return;
    //        }

    //    }
    //    print(num);
    //    print(createDir.ToString());
    //    CurrPoint = CurrPoint + createDir;
    //    mapArray[CurrPoint.X, CurrPoint.Y] = true;
    //    CreateARoom(CurrPoint.X, CurrPoint.Y, "name");
    //    depth--;
    //    CreateToWay(way, depth);
    //}

    private void InitRooms()
    {
        for(int i = 0; i < 50; i++)
        {
            GameObject map = Instantiate(roomPrefab.gameObject);
            if(i == 0)
            {
                map.transform.parent = this.transform;
                mapWidth = map.GetComponent<Tilemap>().localBounds.size.x * 1.2f * 2.2f;
                mapHeight = map.GetComponent<Tilemap>().localBounds.size.y * 1.2f * 2.2f;   
                map.transform.parent = null;
            }
            map.SetActive(false);
            roomStack.Push(map.GetComponent<DungeonRoom>());
        }
        mapArray = new bool[20, 20];
        coorX = 9;
        coorY = 9;
    }

    //private void CreateDungeonRooms(ref int level, ref int depth, Point createPt, Point fromPt, bool first)
    //{
    //    if (level > depth) return;

    //    // create room.
    //    DungeonRoom room = CreateARoom(createPt, "room" + level, roomStack, mapArray);
    //    int doorway = Point.PointToPointWay(fromPt, createPt);
    //    room.DoorOpen(OppositeDoorNumber(doorway));
    //    room.SetDoorDir(OppositeDoorNumber(doorway), DoorDir.StartWay);
    //    roomDict.Add(createPt, room);

    //    // create point
    //    fromPt = new Point(createPt);
    //    while (true)
    //    {
    //        int way = Random.Range(0, 4);
    //        createPt = Point.GetRandomWay(fromPt);
    //        if(!mapArray[createPt.X, createPt.Y])
    //        {
    //            break;
    //        }
    //    }
    //    Debug.Log(createPt);

    //    if (first)
    //    {
    //        first = false;
    //        room.gameObject.name = "Origin Room";
    //        room.DoorClose(OppositeDoorNumber(doorway));
    //        room.SetDoorDir(OppositeDoorNumber(doorway), DoorDir.None);
    //    }

    //    level++;
    //    if (level < depth)
    //    {
    //        int opendoor = Point.PointToPointWay(fromPt, createPt);
    //        room.DoorOpen(opendoor);
    //        room.SetDoorDir(opendoor, DoorDir.BossWay);
    //    }
    //    else if(level == depth)
    //    {
    //        room.gameObject.name = "Boss Room";
    //    }


    //    CreateDungeonRooms(ref level, ref depth, createPt, fromPt, first);
    //}

    public void CreateRooms(int depth, Point createPt, Point fromPt, int enterDir, int outDir, bool isMain)
    {
        if (depth <= 0) return;

        // base
        DungeonRoom room = CreateARoom(createPt, "room", roomStack, mapArray);
        room.SetRoomCoord(createPt.X, createPt.Y);
        roomDict.Add(createPt, room);

        depth--;

        // enter door
        if (fromPt == null)
        {
            room.gameObject.name = "Origin Room";
        }
        else
        {
            room.setDoorProperties(OppositeDoorNumber(enterDir), true, DoorDir.StartWay);

            room.Doors[OppositeDoorNumber(enterDir)].SetNextRoom(roomDict[fromPt]);
            roomDict[fromPt].Doors[enterDir].SetNextRoom(room);

            int num = Random.Range(0, 5);
            GameObject inner = Instantiate(InnerPattern[num]);
            inner.transform.position = room.transform.position;
            inner.transform.parent = room.transform;
        }

        fromPt = new Point(createPt);
        while (true)
        {
            int way = Random.Range(0, 4);
            createPt = Point.WayToPoint(way) + fromPt;
            if (!mapArray[createPt.X, createPt.Y])
            {
                enterDir = way;
                outDir = OppositeDoorNumber(enterDir);
                if (depth == 0 && isMain)
                {
                    room.name = "Boss Room";
                    break;
                }
                else if(depth == 0 && !isMain)
                {
                    room.name = "Last Room";
                    break;
                }
                room.setDoorProperties(OppositeDoorNumber(outDir), true, DoorDir.BossWay);
                //room.Doors[OppositeDoorNumber(outDir)].SetNextRoom(roomDict[createPt]);
                //Debug.Log("out dir is " + outDir + ", enter dir is " + enterDir);
                break;
            }
        }

        CreateRooms(depth, createPt, fromPt, enterDir, outDir, isMain);

        // create branch
        if (depth == 5 && isMain)
        {
            Point currPt = fromPt;
            // find empty point
       
            int count = 0;
            if (!mapArray[fromPt.X + 1, fromPt.Y]) count++;
            if (!mapArray[fromPt.X - 1, fromPt.Y]) count++;
            if (!mapArray[fromPt.X, fromPt.Y + 1]) count++;
            if (!mapArray[fromPt.X, fromPt.Y - 1]) count++;

            if(count > 0)
            {
                while (true)
                {
                    int way = Random.Range(0, 4);
                    createPt = Point.WayToPoint(way) + fromPt;
                    if (!mapArray[createPt.X, createPt.Y])
                    {
                        enterDir = way;
                        outDir = OppositeDoorNumber(enterDir);
                        room.setDoorProperties(OppositeDoorNumber(outDir), true, DoorDir.BossWay);
                        break;
                    }
                }
                CreateRooms(depth, createPt, fromPt, enterDir, outDir, false);
            }
        }
    }

    //private void CreateDungeon(int depth, int branchCount)
    //{
    //    if (depth < 1 || depth > 100) return;
        
    //    int from = 0;
    //    for(int i = 0; i < depth; i++)
    //    {
    //        CreateRoom(ref from);
    //        if (HasBranch(ref branchCount))
    //        {
    //            //check 4way
    //            List<int> list = new List<int>();
    //            if (!mapArray[beforeCoorX, beforeCoorY + 1]) list.Add(0);
    //            if (!mapArray[beforeCoorX + 1, beforeCoorY]) list.Add(1);
    //            if (!mapArray[beforeCoorX, beforeCoorY - 1]) list.Add(2);
    //            if (!mapArray[beforeCoorX - 1, beforeCoorY]) list.Add(3);
    //            int way = Random.Range(0, list.Count);
    //            Debug.Log("list count : " + list.Count);
    //            DungeonRoom room = null;
    //            switch (way)
    //            {
    //                case 0:
    //                    room = CreateARoom(beforeCoorX, beforeCoorY + 1, "branch room");
    //                    mapArray[coorX, coorY + 1] = true;
    //                    break;
    //                case 1:
    //                    room = CreateARoom(beforeCoorX + 1, beforeCoorY, "branch room");
    //                    mapArray[coorX + 1, coorY] = true;
    //                    break;
    //                case 2:
    //                    room = CreateARoom(beforeCoorX, beforeCoorY - 1, "branch room");
    //                    mapArray[coorX, coorY - 1] = true;
    //                    break;
    //                case 3:
    //                    room = CreateARoom(beforeCoorX - 1, beforeCoorY, "branch room");
    //                    mapArray[coorX - 1, coorY] = true;
    //                    break;
    //            }
    //            room.DoorOpen(OppositeDoorNumber(way));
    //        }
            
    //    }
    //}

    //private void CreateRoom(ref int from)
    //{
    //    string name = string.Format("number: {0} room", ++roomNumber);
    //    DungeonRoom createdRoom = CreateARoom(new Point(coorX, coorY), name, roomStack, mapArray);
    //    while (true)
    //    {
    //        int dir = Random.Range(0, 3);
    //        if (IsCorrectWay(dir, from, mapArray))
    //        {
    //            createdRoom.DoorOpen(OppositeDoorNumber(from));
    //            from = dir;
    //            createdRoom.DoorOpen(from);
    //            roomDict.Add(new Point(coorX, coorY), createdRoom);
    //            break;
    //        }
    //    }
    //}

    private DungeonRoom CreateARoom(int x, int y, string name)
    {
        DungeonRoom room = roomStack.Pop();
        room.gameObject.name = name;
        room.gameObject.SetActive(true);
        room.transform.parent = this.transform;
        room.gameObject.transform.position = new Vector3(x * mapWidth, y * mapHeight);
        mapArray[x, y] = true;
        return room;
    }

    private DungeonRoom CreateARoom(Point pt, string name, Stack<DungeonRoom> stack, bool[,] array)
    {
        DungeonRoom room = stack.Pop();
        room.gameObject.name = name;
        room.gameObject.SetActive(true);
        room.transform.parent = this.transform;
        room.transform.position = new Vector3(pt.X * mapWidth, pt.Y * mapHeight);
        room.LeftDoorWay = DoorDir.None;
        room.RightDoorWay = DoorDir.None;
        room.UpperDoorWay = DoorDir.None;
        room.BottomDoorWay = DoorDir.None;
        array[pt.X, pt.Y] = true;
        return room;
    }

    private bool IsCorrectWay(int way, int from, bool[,] array)
    {
        //if (way == from) return false;
        int x = coorX;
        int y = coorY;
        switch (way)
        {
            case 0: y += 1; break;
            case 1: x += 1; break;
            case 2: y -= 1; break;
            case 3: x -= 1; break;
        }

        if(array[x, y])
        {
            return false;
        }
        else
        {
            beforeCoorX = coorX;
            beforeCoorY = coorY;
            switch (way)
            {
                case 0: coorY += 1; break;
                case 1: coorX += 1; break;
                case 2: coorY -= 1; break;
                case 3: coorX -= 1; break;
            }
            return true;
        }
    }

    public int OppositeDoorNumber(int way)
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
    
    private bool HasBranch(ref int branchCount)
    {
        if (branchCount < 1) return false;
        if(Random.Range(0f,1f) > 0.6f)
        {
            branchCount--;
            return true;
        }
        return false;
    }

}


