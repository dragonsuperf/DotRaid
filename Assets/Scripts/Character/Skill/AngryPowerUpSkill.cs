using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 분노 - 액티브
/// </summary>
[SkillJobKind(SkillJobKindAttribute.eAttribute.Warrior)]
public class AngryPowerUpSkill : BuffSkillBase
{
    [Header("Skill Option")]
    [SerializeField] private float _upDamage = 1.1f; //퍼센트
    [SerializeField] private float _selfHeal = 1.1f; //퍼센트
    [SerializeField] private float _selfHit = 1.01f; //퍼센트

    private float _originAttack;

    private int _attackCount = 0;

    public override void OnSet(object data)
    {
        base.OnSet(data);
        if(GameManager.Instance.chracters[_data.player_info.idx] == null)
        {
            Debug.Log("캐릭터 인덱스가 없음 넘겼는지 확인좀");
            return;
        }
        Data.inherentCallback = AnglerPowerEffect; //타이밍이 필요한 고유 능력적용
        
        _originAttack = GameManager.Instance.chracters[_data.player_info.idx].CharPhysicDamage; // TODO 이 동안 데미지가 증가하면 문제가 있다.
        GameManager.Instance.chracters[_data.player_info.idx].CharPhysicDamage = (int)((float)_originAttack * _upDamage);
    }

    private void AnglerPowerEffect(Enemy enemy)
    {
        _attackCount++;
        _attackCount %= 3;
        if(_attackCount == 0)
        {
            //3번 치면 자힐
            GameManager.Instance.chracters[_data.player_info.idx].TakeHeal(1); 
        }
        else
        {
            GameManager.Instance.chracters[_data.player_info.idx].TakeDamage(1);
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
        GameManager.Instance.chracters[_data.player_info.idx].CharPhysicDamage = _originAttack;
    }
}
