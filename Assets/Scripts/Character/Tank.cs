using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Character
{
    GameObject defencer;
    //CharacterStats defencerStat;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
        
        //charSkill = GameObject.Find("CharSkill").GetComponent<CharSkill>();
        //defencerStat = stat;
    }
    

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //Debug.Log("Defencer : " + stat.armor);
    }


    
    
}
