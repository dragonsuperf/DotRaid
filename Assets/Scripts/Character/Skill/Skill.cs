using UnityEngine;
using System;
using System.Collections.Generic;

public enum eTargetState
{
    Enemy = 0,
    Charactor,
    Ground
}

public enum eSkillState
{
    Target = 0,
    NonTarget,
    Range
}

public class TargetInfo
{
    public int idx;
    public eTargetState state;
    public Vector2 pos; //생성시점 위치가 됨
}

/// <summary>
/// 스킬은 플레이어 정보만 있으면 된다
/// </summary>
public class SkillData
{
    public int player_idx = -1;
    public TargetInfo target_info = null;
    public eSkillState state = eSkillState.Target;
    public List<Effect> effect = null;
    public Action createEffectCallback = null; //생성 시점
    public Action hitEffectCallback = null; //히팅 시점
}

public class Skill : MonoBehaviour
{
    //스킬 id임
    protected int _idx = -1;
    protected SkillData _data;
    public SkillData Data { get { return _data; } private set { } }
    public int IDX { get { return _idx; } set { _idx = value; } }
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
