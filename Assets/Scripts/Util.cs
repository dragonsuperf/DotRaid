﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public static class Util
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

    public static void DrawCircle(GameObject container, float radius, float lineWidth)
    {
        var segments = 360;
        GameObject circle = new GameObject();
        circle.transform.SetParent(container.transform);
        
        var line = circle.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.SetPositions(points);
        circle.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public static int DirFileCount(DirectoryInfo d)
    {
        int i = 0;
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            if (!fi.Extension.Contains(".meta")) // metafile 제외
                i++;
        }
        return i;
    }
}