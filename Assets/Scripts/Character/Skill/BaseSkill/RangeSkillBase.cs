using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 롤처럼 범위 있는 스킬
/// </summary>
public class RangeSkillBase : Skill
{
    public override void OnSet(object data)
    {
        _data = (SkillData)data;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
