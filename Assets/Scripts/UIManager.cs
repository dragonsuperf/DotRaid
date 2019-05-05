using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    Texture2D selectTexture;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //------------RTS 스타일 선택사각형
    public Texture2D Texture
    {
        get
        {
            if (selectTexture == null)
            {
                selectTexture = new Texture2D(1, 1);
                selectTexture.SetPixel(0, 0, Color.white);
                selectTexture.Apply();
            }
            return selectTexture;
        }
    }

    public void DrawSelectRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, Texture);
        GUI.color = Color.white;
    }

    public void DrawSelectRectBorder(Rect rect, float thickness, Color color)
    {
        // Top
        DrawSelectRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Left
        DrawSelectRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Right
        DrawSelectRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Bottom
        DrawSelectRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    public Rect GetSelectRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    //-----------RTS 스타일 선택 사각형


    public Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }
}
