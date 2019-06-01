using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Astar;
    private AstarPath AstarPath;

    private void Start(){
        Init();
        AttachAstar(DungeonManager.Instance.GetCurrentDungeonRoom());
    }

    public void Init(){
        GameObject go = Instantiate(Astar);
        go.transform.parent = this.transform;
        AstarPath = go.GetComponent<AstarPath>();
    }

    public void AttachAstar(DungeonRoom room){
        AstarPath.transform.parent = room.gameObject.transform;
        AstarData  data = AstarPath.active.data;
        GridGraph gridGraph = data.gridGraph;
        // gridGraph.width = 80;
        // gridGraph.depth = 60;
        gridGraph.SetDimensions(80, 60, 1f);
        gridGraph.center = room.gameObject.transform.position;
        gridGraph.Scan();
        // Debug.Log(gridGraph.center);
        
        // Debug.Log(gridGraph.center);
    }

    
}
