using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class CutSceneManager : MonoBehaviour
{
    List<GameObject> imgList = new List<GameObject>();
    int stage = 1;
    // Start is called before the first frame update
    void Start()
    {
        int files = Util.DirFileCount(new DirectoryInfo("Assets/Resources/UIs/Stage" + stage));
        // Need To Call Images
        for (int i = 1; i <= files; i++)
        {
            imgList.Add(Instantiate(Resources.Load("UIs/Stage1/Scene_" + i) as GameObject, transform));
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) 
        {
            if (imgList.Count > 0)
            {
                //Destroy(imgList[imgList.Count - 1]);
                StartCoroutine(DestroyWithFadeOut(imgList[imgList.Count - 1].GetComponent<Image>()));
                imgList.RemoveAt(imgList.Count - 1);
            }
            // else
            // 씬 넘어감
        }
    }

    IEnumerator DestroyWithFadeOut(Image obj)
    {
        Color tmp = obj.color;

        for (; tmp.a > 0; tmp.a -= 0.15f)
        {
            obj.color = tmp;
            yield return new WaitForSeconds(0.1f);
        }

        Object.Destroy(obj);
        yield return new WaitForSeconds(0.1f);
    }
}
