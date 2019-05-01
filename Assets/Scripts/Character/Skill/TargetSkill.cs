using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타겟팅 되는 스킬
/// </summary>
public class TargetSkill : Skill
{
    public override void OnSet(int idx)
    {
        base.OnSet(idx);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }

    protected virtual void CreatePosition(Vector2 position)
    {
        transform.position = position;
    }

    protected virtual void MoveToTarget()
    {

    }
}
