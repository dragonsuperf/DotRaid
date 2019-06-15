using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalRangeSkill : TargetSkillBase
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _turnSpeed = 0.1f;

    public override void OnSet(object data)
    {
        _data = (SkillData)data;
        CreatePosition(_data.player_info.pos);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        SetDirection(transform.position, _data.target_info.pos); // TODO _data.target_info.pos 는 타겟 시작 위치임 적의 위치 갱신 필요 
        SetLerpRotate(_turnSpeed , _data.target_info.pos);
        MoveToPosition(_dir, _speed);
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Data.hitCallback.SafeInvoke();
            SkillManager.Instance.Remove(this._idx);
        }
    }
}
