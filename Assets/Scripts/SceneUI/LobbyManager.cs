using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players = new GameObject[5];
    [SerializeField]
    private GameObject[] portals = new GameObject[2];
    private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCharacters(Vector3 to){
        foreach(GameObject ch in players){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(ch.transform.position, to, step);
        }
    }
}
