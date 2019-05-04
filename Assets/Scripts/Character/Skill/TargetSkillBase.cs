using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타겟팅 되는 스킬
/// </summary>
public class TargetSkillBase : Skill
{
    [Header("EFFECT INFO")]
    [SerializeField] private Effect _ceateEffect = null;
    [SerializeField] private Effect _destoryEffect = null;

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
    
    protected void CreateEffect()
    {
        
    }

    protected virtual void CreatePosition(Vector2 position)
    {
        transform.position = position;
    }

    protected virtual void MoveToTarget()
    {

    }
}
