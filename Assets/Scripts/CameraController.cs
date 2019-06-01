using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] selector;

    public List<Character> selectedUnits = new List<Character>();

    private List<Character> characters;

    private Vector3 CameraPosition;

    private bool strictMode;
    private float cameraAcc;

    // Start is called before the first frame update
    void Start()
    {
        characters = GameManager.Instance.GetChars();
        selector = new GameObject[characters.Count];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

        CameraPosition = new Vector3(selectedUnits[0].transform.position.x, selectedUnits[0].transform.position.y, -10);


        transform.position = CameraPosition;
    }
}

