using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CutSceneManager : MonoBehaviour
{
    List<GameObject> imgList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        // Need To Call Images
        for (int i = 1; i <= 4; i++)
        {
            imgList.Add(Instantiate(Resources.Load("UIs/Stage1/Scene_" + i) as GameObject, transform));
        }
    }
}
