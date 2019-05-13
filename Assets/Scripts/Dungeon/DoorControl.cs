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
            Debug.Log(next);
            var characters = GameManager.Instance.heroes;
            foreach(var ch in characters)
            {
                Vector3 position = next.GetDoorObj(DungeonCreator.Instance.OppositeDoorNumber(doorWay)).transform.position;
                ch.gameObject.transform.position = position;
                ch.gameObject.GetComponent<Character>().charState = CharacterState.idle;
            }
            GameManager.Instance.MoveCameraToRoomPosition(next);
        }
    }
}
