using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 패시브가 필요한 스킬에만 붙는다 ( 광역버프같이 필요한 거 ) 
/// 나머지 거리비례 데미지 같은 스킬은 각 스킬에 옵션으로 붙는다
/// </summary>
public class PassiveSkillBase : Skill
{
    protected Character hero;
    protected int heroIdx = 0;
    /// <summary>
    /// data는 캐릭터 id만 필요.. 나머지 쓰지마샘 설계오류
    /// </summary>
    public override void OnSet(object data = null)
    {
        base.OnSet(data);
        _data = (SkillData)data;
        heroIdx = _data.player_info.idx;
        hero = GameManager.Instance.chracters[heroIdx];
    }

    public override void OnRemove()
    {
        base.OnRemove();
        SkillManager.Instance.Remove(this._idx);
    }
}
