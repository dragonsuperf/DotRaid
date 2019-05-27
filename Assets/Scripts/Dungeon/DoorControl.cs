using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public Door Door;
    public int doorWay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && DungeonManager.Instance.roomCleared)
        {
            DungeonManager.Instance.roomCleared = false;
            DungeonRoom next = Door.NextRoom;
            // Debug.Log(next);
            var characters = GameManager.Instance.heroes;
            foreach(var ch in characters)
            {
                Vector3 position = next.GetDoorObj(DungeonCreator.Instance.OppositeDoorNumber(doorWay)).transform.position;
                ch.gameObject.transform.position = position;
                ch.gameObject.GetComponent<Character>().charState = CharacterState.idle;
            }
            GameManager.Instance.MoveCameraToRoomPosition(next);
            DungeonManager.Instance.SetOffClearToggle();

            DungeonManager.Instance.CurrPoint = GetCurrentPoint(Door, DungeonManager.Instance.CurrPoint);
            DungeonManager.Instance.AdjancentRooms = DungeonManager.Instance.GetAdjacentRooms(DungeonManager.Instance.CurrPoint);
            List<DungeonRoom> rooms = new List<DungeonRoom>();
            rooms.Add(DungeonCreator.Instance.Rooms[DungeonManager.Instance.CurrPoint]);
            foreach(DungeonRoom room in DungeonManager.Instance.AdjancentRooms){
                rooms.Add(room);
            }
            foreach(KeyValuePair<Point, DungeonRoom> pair in DungeonCreator.Instance.Rooms){
                if(!rooms.Contains(pair.Value)){
                    pair.Value.gameObject.SetActive(false);
                }
                else{
                    pair.Value.gameObject.SetActive(true);
                }
            }
        }
    }

    private Point GetCurrentPoint(Door door, Point currPoint){
        Direction dir = door.DoorDirection;
        switch(dir){
            case Direction.Bottom:
                currPoint += new Point(0, -1);
            break;
            case Direction.Right:
                currPoint += new Point(1, 0);
            break;
            case Direction.Left:
                currPoint += new Point(-1, 0);
            break;
            case Direction.Up:
                currPoint += new Point(0, 1);
            break;
        }
        return currPoint;
    }
}
