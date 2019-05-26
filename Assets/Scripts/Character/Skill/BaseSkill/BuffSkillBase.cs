using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 지속시간 동안 능력이 상승하는 버프 스킬 base
/// </summary>
public class BuffSkillBase : Skill
{
    [SerializeField] protected float _duringTime = 5.0f;
    private float _startTime = 0;

    /// <summary>
    /// 생성 타이밍에 버프 생성
    /// </summary>
    public override void OnSet(object data)
    {
        _data = (SkillData)data;
        _startTime = Time.time;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(_startTime + _duringTime < Time.time)
        {
            //자신 삭제
            SkillManager.Instance.Remove(_idx);
        }
    }

    /// <summary>
    /// 소멸 타이밍에 버프 삭제
    /// </summary>
    public override void OnRemove()
    {
        base.OnRemove();
    }
}
