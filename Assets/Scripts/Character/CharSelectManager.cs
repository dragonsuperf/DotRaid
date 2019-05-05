using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectManager : MonoBehaviour
{
    GameManager gameManager;
    public UIManager uiManager;
    public GameObject selectedUnit;
    public GameObject[] selector;
    public List<Character> selectedUnits = new List<Character>();
    RaycastHit hit;
    private Vector3 mouseDownPoint, currentDownPoint;
    public bool isSelecting = false;
    Vector3 mousePosition;
    public Character[] characters;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        characters = gameManager.GetChars();
        selector = new GameObject[characters.Length];

        for (int i = 0; i < selector.Length; i++)
        {
            //selector[i] = characters[i].transform.Find("arrowSelector").gameObject;
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
            var rect = uiManager.GetSelectRect(mousePosition, Input.mousePosition);
            uiManager.DrawSelectRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            uiManager.DrawSelectRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    public void IsWithinSelectionBounds()
    {
        var camera = Camera.main;
        var viewportBounds =
            uiManager.GetViewportBounds(camera, mousePosition, Input.mousePosition);

        for (int i = 0; i < characters.Length; i++)
        {
            if (viewportBounds.Contains(camera.WorldToViewportPoint(characters[i].transform.position)))
            {
                selectedUnits.Add(characters[i]);
                //characters[i].transform.Find("arrowSelector").gameObject.SetActive(true);
            }
        }

    }

}
