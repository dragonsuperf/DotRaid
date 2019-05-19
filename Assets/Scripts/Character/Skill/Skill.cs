using UnityEngine;
using System;
using System.Collections.Generic;

public enum eSkill
{
    TestSkill = 0,
    Sniping
}

/// <summary>
/// 타겟 정보
/// </summary>
public enum eTargetState
{
    Enemy = 0,
    Charactor,
    Ground
}

/// <summary>
/// 스킬 정보 분류 ( 캐릭터 시점 스킬 생성 관련 )
/// </summary>
public enum eSkillState
{
    None = -1,
    Nomal = 0, //원딜 일반 공격 ( 유도탄? )
    NonTarget_Cast, //캐스팅 후 특정 방향으로 발사
    One_Effect, //생성되어 한 번 효과를 발휘하고 사라지는 형식
    Keep_Effect_Cast, //생성되어 버프, 공격 하는 형식 ( 캐스팅 필요 )
    Channeling //캐스팅중 지속해서 특정 스킬을 사용 하는 형식
}

public class CasterInfo
{
    public int idx;
    public Vector2 pos; //생성되는 위치가 됨
}

public class TargetInfo
{
    public int idx;
    public eTargetState state;
    public Vector2 pos; //생성시점 타겟 위치가 됨
}

/// <summary>
/// 스킬은 플레이어 정보만 있으면 된다
/// </summary>
public class SkillData
{
    public CasterInfo player_info = null;
    public TargetInfo target_info = null;
    public eSkillState state = eSkillState.Nomal;
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
