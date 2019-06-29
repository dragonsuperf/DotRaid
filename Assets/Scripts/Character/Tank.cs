using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Character
{
    [Header("Passive Effect Distance")] public float passiveDist = 1.0f; 
    private DefencerPassiveSkill _passiveSkill = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //make passive ex
        {
            SkillData sd = new SkillData();
            sd.player_info = new CasterInfo();
            sd.player_info.idx = base._idx; // @NOTE 플레이어 idx만 필요함
            _passiveSkill = SkillManager.Instance.CreatePassive<DefencerPassiveSkill>(sd) as DefencerPassiveSkill;
            _passiveSkill.Init(passiveDist);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
