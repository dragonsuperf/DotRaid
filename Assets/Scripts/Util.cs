using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GameUtil
{
    static public Vector2 GetDirection(Vector2 startPos, Vector2 targetPos)
    {
        Vector2 dir = targetPos - startPos;
        dir.Normalize();
        return dir;
    }
    static public Transform GetChildWithName(GameObject obj, string name)
    {
        foreach (Transform t in obj.transform)
        {
            if (t.name == name)
                return t;
        }
        Debug.Log("There is no object named : " + name);
        return null;
    }
}