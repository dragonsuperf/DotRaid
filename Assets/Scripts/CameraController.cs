﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    private List<Character> characters;

    private Vector3 CameraPosition;

    private Vector3 mousePosition;

    private InputListener InListner;

    private const int UP = 1, DONW = 2, LEFT = 3, RIGHT = 4, LUP = 5, LDONW = 6, RUP = 7, RDOWN = 8, NONE = 0;

    private int[] idx;
    private int[] idy;
    private int state;

    private float a, b;

    private const float tf = 4;//테스트용
    private const float senseRound = 30; //마우스 인식범위
    private float vi;//화면비율

    private GameObject currentRoom;

    private float RoomWidth;
    private float RoomHeight;

    private float RoomMinX;
    private float RoomMinY;

    // Start is called before the first frame update
    void Start()
    {

        characters = GameManager.Instance.GetChars();
        CameraPosition = transform.position;
        currentRoom = GameObject.Find("Origin Room");
        if (currentRoom == null)
        {
            print("방읽기실패");
            return;
        }

        state = NONE;
        idx = new int[9] { 0, 0, 0, -1, 1, -1, -1, 1, 1 };
        idy = new int[9] { 0, -1, 1, 0, 0, -1, 1, -1, 1 };

        RoomMinX = -15.65f;
        RoomMinY = -15;
        RoomWidth = 17.65f;
        RoomHeight = 12;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.mousePosition.x > Screen.width - senseRound)
        {

            if (Input.mousePosition.y < senseRound)
            {
                state = RUP;
            }
            else if (Input.mousePosition.y > Screen.height - senseRound)
            {
                state = RDOWN;
            }
            else
            {
                state = RIGHT;
            }

        }

        else if (Input.mousePosition.x < senseRound)
        {

            if (Input.mousePosition.y < senseRound)
            {
                state = LUP;
            }
            else if (Input.mousePosition.y > Screen.height - senseRound)
            {
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
                state = UP;
            }
            else if (Input.mousePosition.y > Screen.height - senseRound)
            {
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


        if (currentRoom.transform.position.x + RoomMinX > transform.position.x)
        {
            if (state == LEFT || state == LUP || state == LDONW) state = NONE;
        }

        else if (currentRoom.transform.position.x + RoomWidth < transform.position.x)
        {
            if (state == RIGHT || state == RUP || state == RDOWN) state = NONE;
        }

        if (currentRoom.transform.position.y + RoomHeight < transform.position.y)
        {
            if (state == RDOWN || state == LDONW || state == DONW) state = NONE;
        }

        else if (currentRoom.transform.position.y + RoomMinY > transform.position.y)
        {
            if (state == UP || state == LUP || state == RUP) state = NONE;
        }

        transform.position = new Vector3(transform.position.x + idx[state], transform.position.y + idy[state], -10);
    }

    private void CameraToHero(int keycode)
    {
        float x, y;

        x = characters[keycode].gameObject.transform.position.x;
        y = characters[keycode].gameObject.transform.position.y;

        if (currentRoom.transform.position.x + RoomMinX > x)
        {
            x = currentRoom.transform.position.x + RoomMinX;
        }

        else if (currentRoom.transform.position.x + RoomWidth < x)
        {
            x = currentRoom.transform.position.x + RoomWidth;
        }

        if (currentRoom.transform.position.y + RoomHeight < y)
        {
            y = currentRoom.transform.position.y + RoomHeight;
        }

        else if (currentRoom.transform.position.y + RoomMinY > y)
        {
            y = currentRoom.transform.position.y + RoomMinY;
        }

        transform.position = new Vector3(x, y, -10);


    }
}



