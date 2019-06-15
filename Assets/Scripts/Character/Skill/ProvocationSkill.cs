using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 디펜서 - 도발
/// </summary>
[SkillJobKind(SkillJobKindAttribute.eAttribute.Tanker)]
public class ProvocationSkill : BuffSkillBase
{
    public override void OnSet(object data)
    {
        base.OnSet(data);
        if (GameManager.Instance.chracters[_data.player_info.idx] == null)
        {
            Debug.Log("캐릭터 인덱스가 없음 넘겼는지 확인좀");
            return;
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
