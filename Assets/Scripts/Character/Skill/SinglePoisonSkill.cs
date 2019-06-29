using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SkillJobKind(SkillJobKindAttribute.eAttribute.Warrior)]
public class SinglePoisonSkill : BuffSkillBase
{
    public override void OnSet(object data)
    {
        base.OnSet(data);
        if (GameManager.Instance.chracters[_data.player_info.idx] == null)
        {
            Debug.Log("캐릭터 인덱스가 없음 넘겼는지 확인좀");
            return;
        }
        Data.inherentCallback = HitPoison;
    }

    /// <summary>
    /// 도트 중첩시 도트시간만 초기화
    /// </summary>
    private void HitPoison()
    {
        GameManager.Instance.Enemies[Data.target_info.idx].StopDotCorotine();
        StartCoroutine( GameManager.Instance.Enemies[ Data.target_info.idx ].StartTakeDotDamage(1,1,10,DamageType.physic) );
        SkillManager.Instance.Remove(IDX);
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
