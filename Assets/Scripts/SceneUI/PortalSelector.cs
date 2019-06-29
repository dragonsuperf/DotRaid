using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSelector : MonoBehaviour
{
    private BoxCollider2D col;
    [SerializeField]
    private LobbyManager lobbyMng;
    private bool isClick = false;

    // Start is called before the first frame update
    void Start()
    {
        col = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && !isClick){
            // Debug.Log("click");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            Vector3 cameraPos = Camera.main.transform.position;
            if(hit.collider != null && hit.collider.gameObject.name == this.gameObject.name){
                Debug.Log("hit");
                isClick = true;
                lobbyMng.MoveCharacters(hit.point);
            }
        }
    }


}
