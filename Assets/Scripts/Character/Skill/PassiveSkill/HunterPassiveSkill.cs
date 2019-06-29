using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 헌터 - 패시브
/// 거리에 따라서 데미지 증가
/// </summary>
[SkillJobKind(SkillJobKindAttribute.eAttribute.Ranger)]
public class HunterPassiveSkill : PassiveSkillBase
{
    public bool isRangeDamageUp = false;
    public override void OnSet(object data = null)
    {
        base.OnSet(data);
        isRangeDamageUp = true;
    }

    public override void OnRemove()
    {
        isRangeDamageUp = false;
        base.OnRemove();
    }
}
