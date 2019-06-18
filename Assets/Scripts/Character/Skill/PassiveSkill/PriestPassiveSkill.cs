using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 홀리 - 패시브
/// 기본 공격이 마나수정을 얻을 확률이 높아진다
/// </summary>
[SkillJobKind(SkillJobKindAttribute.eAttribute.Healer)]
public class PriestPassiveSkill : PassiveSkillBase
{
    private float manaGetPercent = 1.1f;
    public float ManaGetPercent { get { return manaGetPercent; } }
}
