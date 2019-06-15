using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스나이퍼 - 액티브 스킬
/// </summary>
[SkillJobKind(SkillJobKindAttribute.eAttribute.Ranger)]
public class SnipingSkill : NonTargetSkillBase
{
    [SerializeField] private float _speed = 1.0f;

    public override void OnSet(object data)
    {
        base.OnSet(data);
        _data.createEffectCallback = CreateEffect;

        CreatePosition(_data.player_info.pos);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        MoveToPosition(_dir, _speed);
    }

    public override void OnRemove()
    {
        base.OnRemove();
        Debug.Log(_idx + " 번 스킬 지워짐");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
            SkillManager.Instance.Remove(this._idx);
    }
}
