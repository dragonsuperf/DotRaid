using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이스 - 패시브
/// 액티브 스킬이 슬로우 효과를 준다
/// </summary>
[SkillJobKind(SkillJobKindAttribute.eAttribute.Ranger)]
public class IceMagePassiveSkill : PassiveSkillBase
{
    public bool isActiveSlowEffect = false;
    public override void OnSet(object data = null)
    {
        base.OnSet(data);
        isActiveSlowEffect = true;
    }
}
