using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 논 타겟 스킬
/// </summary>
public class NonTargetSkillBase : Skill
{
    protected Vector2 _dir = Vector2.zero; //방향 단위벡터

    public override void OnSet(object data)
    {
        _data = (SkillData)data;
        SetDirection(_data.player_info.pos, _data.target_info.pos);
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

    protected virtual void SetDirection(Vector2 startPos, Vector2 targetPos)
    {
        _dir = targetPos - startPos;
        _dir.Normalize();
        Debug.Log(targetPos + " " + startPos + " " +_dir);
    }

    protected virtual void MoveToPosition(Vector2 dir , float speed)
    {
        transform.Translate(dir * speed * Time.deltaTime, Space.World);
    }
}
