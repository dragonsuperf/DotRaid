using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : TargetSkill
{
    public override void OnSet(int idx)
    {
        base.OnSet(idx);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(Input.GetKeyDown(KeyCode.A))
        {
            SkillManager.Instance.Remove(_idx);
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
        Debug.Log(_idx + " 번 스킬 지워짐");
    }
}
