using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타겟팅 되는 스킬
/// </summary>
public class TargetSkillBase : Skill
{
    protected Vector2 _dir = Vector2.zero; //방향 단위벡터

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

    protected virtual void SetDirection(Vector2 curPos, Vector2 targetPos)
    {
        _dir = targetPos - curPos;
        _dir.Normalize();
    }

    protected void SetLerpRotate(float turnSpeed , Vector2 targetPos)
    {
        if (_dir != Vector2.zero)
        {
            var angle = Mathf.LerpAngle(transform.eulerAngles.z , -GetAngle(transform.position.x, transform.position.y, targetPos.x, targetPos.y) + 180.0f, 0.1f);

            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    protected virtual void MoveToPosition(Vector2 dir, float speed)
    {
        transform.Translate(dir * speed * Time.deltaTime, Space.World);
    }

    protected float GetAngle(float x1, float y1, float x2, float y2)
    {
        float dx = x2 - x1;
        float dy = y2 - y1;

        float rad = Mathf.Atan2(dx, dy);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }
}
