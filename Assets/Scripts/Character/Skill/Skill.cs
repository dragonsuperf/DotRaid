using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 스킬은 플레이어 정보만 있으면 된다
/// </summary>
public class SkillData
{
    public int idx = -1;
    public int player_idx = -1;
    public List<Effect> effect = null;
    public Action createEffectCallback = null; //생성 이펙트
    public Action hitEffectCallback = null; //히팅 이펙트
}

public class Skill : MonoBehaviour
{
    //스킬 id임
    protected SkillData _data;
    public SkillData Data { get { return _data; } private set { } }
    
    /// <summary>
    /// 초기화임
    /// </summary>
    public virtual void OnSet(object data)
    {
        _data = (SkillData)data;
    }
    /// <summary>
    /// 매니저에서 업데이트로 불림
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    /// 마지막에 지워질 때 불림
    /// </summary>
    public virtual void OnRemove() { }
}
