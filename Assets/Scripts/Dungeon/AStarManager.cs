using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarManager : Singleton<AStarManager>
{
    private GameObject Astar;
    private DungeonRoom privious;
    public AStarGrid AStarGrid;

    protected override void Start(){
        base.Start();
        Astar = Instantiate(Resources.Load("Prefabs/Maps/aStar") as GameObject);
        Init();
        AttachAstar(DungeonManager.Instance.GetCurrentDungeonRoom());
        privious = DungeonManager.Instance.GetCurrentDungeonRoom();
    }

    public void Update()
    {
        if(privious != DungeonManager.Instance.GetCurrentDungeonRoom())
        {
            Debug.Log("Room Change");
            privious = DungeonManager.Instance.GetCurrentDungeonRoom();
            AStarGrid.BuildGrid();
        }    
    }

    public void Init(){
        GameObject go = Instantiate(Astar);
        go.transform.parent = this.transform;
        AStarGrid = go.GetComponent<AStarGrid>();
    }

    public void AttachAstar(DungeonRoom room){
        AStarGrid.transform.parent = room.gameObject.transform;
        AStarGrid.transform.position = room.gameObject.transform.position;      
        AStarGrid.collidableMap = room.GetComponent<Tilemap>();
        
        //AStarGrid.collidableMap = room.gameObject.transform.Find("Pattern").GetComponent<Tilemap>();

        //collidableMap = DungeonManager.Instance.GetCurrentDungeonRoom()
        //    .transform.Find("Pattern").GetComponent<Tilemap>();

        //AstarData  data = AstarPath.active.data;
        //GridGraph gridGraph = data.gridGraph;
        // gridGraph.width = 80;
        // gridGraph.depth = 60;
        //gridGraph.SetDimensions(80, 60, 1f);
        //gridGraph.center = room.gameObject.transform.position;
        //gridGraph.Scan();
        // Debug.Log(gridGraph.center);

        // Debug.Log(gridGraph.center);
    }

}
