using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackEffectChanger : MonoBehaviour {

    Animator parentAni;
    Animator ani;
	void Start () {
        parentAni = transform.parent.GetComponent<Animator>();
        ani = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (parentAni.GetBool("cast"))
        {
            ani.SetBool("cast", true);
        }
        else ani.SetBool("cast", false);

    }
}
