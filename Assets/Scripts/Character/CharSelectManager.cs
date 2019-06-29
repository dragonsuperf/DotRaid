using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelectManager : Singleton<CharSelectManager>
{
    private UIHelper _uiHelper;

    public GameObject[] selector;
    
    public bool isSelecting = false;

    public List<Character> selectedUnits = new List<Character>();

    private List<Character> characters;
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name != "Lobby"){
            characters = GameManager.Instance.GetChars();
            selector = new GameObject[characters.Count];
            _uiHelper = GameManager.Instance.uiHelper;
            for (int i = 0; i < selector.Length; i++)
            {
                //selector[i] = characters[i].transform.Find("arrowSelector").gameObject;
            }
        }else{
            _uiHelper = GameObject.FindGameObjectWithTag("LobbyUI").GetComponent<UIHelper>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            mousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < selector.Length; i++)
            {
                //selector[i].SetActive(false);
            }
            if (isSelecting)
            {
                selectedUnits.Clear();
                IsWithinSelectionBounds();
            }
            isSelecting = false;
        }

        //------------부대-------------------

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedUnits.Clear();
            selectedUnits.Add(characters[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedUnits.Clear();
            selectedUnits.Add(characters[1]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedUnits.Clear();
            selectedUnits.Add(characters[2]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedUnits.Clear();
            selectedUnits.Add(characters[3]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedUnits.Clear();
            selectedUnits.Add(characters[4]);
        }
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            var rect = _uiHelper.GetSelectRect(mousePosition, Input.mousePosition);
            _uiHelper.DrawSelectRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            _uiHelper.DrawSelectRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    /// <summary>
    /// 사각형 안에 있는거 확인?
    /// </summary>
    public void IsWithinSelectionBounds()
    {
        var camera = Camera.main;
        var viewportBounds =
            _uiHelper.GetViewportBounds(camera, mousePosition, Input.mousePosition);

        for (int i = 0; i < characters.Count; i++)
        {
            if (viewportBounds.Contains(camera.WorldToViewportPoint(characters[i].transform.position)))
            {
                selectedUnits.Add(characters[i]);
            }
        }
    }

}
