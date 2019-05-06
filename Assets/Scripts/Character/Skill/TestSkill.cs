using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : TargetSkillBase
{
    public override void OnSet(object data)
    {
        base.OnSet(data);
        _data.createEffectCallback = CreateEffect;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnRemove()
    {
        base.OnRemove();
        Debug.Log(_idx + " 번 스킬 지워짐");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
            SkillManager.Instance.Remove(this._idx);
    }
}
