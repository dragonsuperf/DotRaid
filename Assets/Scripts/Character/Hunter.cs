﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Character
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //make passive ex
        {
            SkillData sd = new SkillData();
            sd.player_info = new CasterInfo();
            sd.player_info.idx = base._idx; // @NOTE 플레이어 idx만 필요함
            SkillManager.Instance.CreatePassive<HunterPassiveSkill>(sd);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
