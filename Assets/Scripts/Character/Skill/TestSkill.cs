using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : TargetSkillBase
{
    public override void OnSet(object idx)
    {
        base.OnSet(idx);
        _data.createEffectCallback = CreateEffect;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnRemove()
    {
        base.OnRemove();
        Debug.Log(_idx + " 번 스킬 지워짐");
    }
}
