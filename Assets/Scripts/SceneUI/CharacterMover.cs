using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private Animator animator;

    private void Start(){
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("walk", true);
    }
}
