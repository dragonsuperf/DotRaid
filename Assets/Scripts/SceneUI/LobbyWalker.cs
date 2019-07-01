using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyWalker : MonoBehaviour
{
    private Animator animator;
    private string[] anim = new string[] { "idle", "walk", "attack" };
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        foreach(var str in anim){
            animator.SetBool(str, false);
        }
        animator.SetBool("idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
