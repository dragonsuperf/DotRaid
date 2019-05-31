using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglePowerUpSkill : BuffSkillBase
{
    [Header("Skill Option")]
    [SerializeField] private float _upDamage = 1.1f; //퍼센트
    [SerializeField] private float _selfHeal = 1.1f; //퍼센트
    [SerializeField] private float _selfHit = 1.01f; //퍼센트

    private int _originAttack;

    private int _attackCount = 0;
    //CharacterStats _tmpStat ;
    public override void OnSet(object data)
    {
        base.OnSet(data);
        if(GameManager.Instance.chracters[_data.player_info.idx] == null)
        {
            Debug.Log("캐릭터 인덱스가 없음 넘겼는지 확인좀");
            return;
        }
        Data.inherentCallback = AnglePowerEffect; //타이밍이 필요한 고유 능력적용
        
        //_originAttack = GameManager.Instance.chracters[_data.player_info.idx].stat.attack;
        //GameManager.Instance.chracters[_data.player_info.idx].stat.attack = (int)((float)_originAttack * _upDamage);
    }

    private void AnglePowerEffect()
    {
        _attackCount++;
        _attackCount %= 3;
        if(_attackCount == 0)
        {
            //3번 치면 자힐
            //GameManager.Instance.chracters[_data.player_info.idx].stat.hp += 1; // TODO 캐릭터쪽 에서 heal 함수로 바꿔야함
        }
        else
        {
            //GameManager.Instance.chracters[_data.player_info.idx].stat.hp -= 1; // TODO 캐릭터쪽 에서 "데미지입었다" 함수로 바꿔야함
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
        //GameManager.Instance.chracters[_data.player_info.idx].stat.attack = _originAttack;
    }
}
