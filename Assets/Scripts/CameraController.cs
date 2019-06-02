using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    private List<Character> characters;

    private Vector3 CameraPosition;

    private Vector3 mousePosition;

    private InputListener InListner;

    private const int UP = 1, DONW = 2 , LEFT = 3, RIGHT = 4, LUP = 5, LDONW = 6, RUP = 7, RDOWN = 8 ,NONE = 0;

    private int[] idx;
    private int[] idy;
    private int state;

    private const float tf=4;//테스트용
    private const float senseRound = 30; //마우스 인식범위
    private float vi;//화면비율

    private GameObject currentRoom;

    // Start is called before the first frame update
    void Start()
    {

        characters = GameManager.Instance.GetChars();
        CameraPosition = transform.position;
        currentRoom = GameObject.Find("Origin Room");
        if (currentRoom == null)
        { 
            print("방읽기실패");
        }
        state = NONE;
        idx = new int[9]{ 0, 0, 0, -1,1, -1,-1, 1, 1};
        idy = new int[9]{ 0, -1,1, 0, 0, -1, 1, -1,1};

        vi = Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        float a = (currentRoom.gameObject.transform.position.x - currentRoom.transform.Find("LeftDoor").transform.position.x) - Camera.main.orthographicSize;
        float b = (currentRoom.gameObject.transform.position.y - currentRoom.transform.Find("BottomDoor").transform.position.y)/2 - Camera.main.orthographicSize;
        a = a / vi /3;
        b = b * vi /3;

        if (Input.mousePosition.x > Screen.width - senseRound)
        {

            if (currentRoom.gameObject.transform.position.x + Camera.main.orthographicSize / 6 < transform.position.x)
            {
                return;
            }

            if (Input.mousePosition.y < senseRound)
            {
                if (currentRoom.transform.position.y+b > transform.position.y) return;
                state = RUP;
            }
            else if (Input.mousePosition.y > Screen.height - senseRound)
            {       
                if (currentRoom.gameObject.transform.position.y < transform.position.y) return;
                state = RDOWN;
            }
            else
            {
                state = RIGHT;
            }

        }

        else if (Input.mousePosition.x < senseRound)
        {        

            if (currentRoom.gameObject.transform.position.x - a > transform.position.x)
            {
                return;
            }

            if (Input.mousePosition.y < senseRound)
            {
                if (currentRoom.transform.position.y+b > transform.position.y) return;
                state = LUP;
            }
            else if (Input.mousePosition.y > Screen.height - senseRound)
            {
                if (currentRoom.gameObject.transform.position.y <= transform.position.y) return;
                state = LDONW;
            }
            else
            {
                state = LEFT;
            }
        }

        else
        {
            if (Input.mousePosition.y < senseRound)
            {
                if (currentRoom.transform.position.y+b > transform.position.y) return;
                state = UP;
            }
            else if (Input.mousePosition.y > Screen.height - senseRound)
            {
                if (currentRoom.gameObject.transform.position.y < transform.position.y)
                {
                    return;
                }

                state = DONW;
            }
            else
            {
                state = NONE;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CameraToHero(0);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CameraToHero(1);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CameraToHero(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CameraToHero(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CameraToHero(4);
        }

        transform.position = new Vector3(transform.position.x + idx[state], transform.position.y + idy[state], -10);
    }

    private void CameraToHero( int keycode )
    {
        float x, y;
        float a = (currentRoom.gameObject.transform.position.x - currentRoom.transform.Find("LeftDoor").transform.position.x) - Camera.main.orthographicSize;
        float b = (currentRoom.gameObject.transform.position.y - currentRoom.transform.Find("BottomDoor").transform.position.y) / 2 - Camera.main.orthographicSize;
        a = a / vi / 3;
        b = b * vi / 3;

        x = characters[keycode].gameObject.transform.position.x;
        y = characters[keycode].gameObject.transform.position.y;

        if (currentRoom.gameObject.transform.position.x + Camera.main.orthographicSize / 6 < x)
        {
            x = currentRoom.gameObject.transform.position.x + Camera.main.orthographicSize / 6;
        }
        if (currentRoom.gameObject.transform.position.y < y)
        {
            y = currentRoom.gameObject.transform.position.y;
        }
        if (currentRoom.transform.position.y + b > y)
        {
            y = currentRoom.transform.position.y + b;
        }
        if (currentRoom.gameObject.transform.position.x - a > x)
        {
            x = currentRoom.gameObject.transform.position.x - a;
        }

        transform.position = new Vector3(x, y, -10);


    }
}



