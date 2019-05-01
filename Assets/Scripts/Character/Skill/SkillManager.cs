using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 여기서 스킬관리
/// </summary>
public class SkillManager : Singleton<SkillManager>
{
    //스킬 다 가지고 있음
    private Dictionary<int, Skill> _skillDict = new Dictionary<int, Skill>();
    private int _skillCount = 0;

    private GameObject _skillRoot = null;

    protected override void Start()
    {
        base.Start();
        _skillRoot = new GameObject("SkillRoot");
    }

    /// <summary>
    /// 스킬 생성함 프리팹이름이 스킬이름이어야함
    /// </summary>
    /// <typeparam name="T">스킬 객체만 ㅇㅋ</typeparam>
    public void Create<T>() where T : Skill
    {
        Type type = typeof(T);
        T loadPrefab = null;
        loadPrefab = Resources.Load("Skill/" + type.ToString().ToLower() ) as T;
        _skillDict.Add(_skillCount, Instantiate<T>(loadPrefab, _skillRoot.transform).GetComponent<Skill>() );
    }

    public bool Delete(int idx)
    {
        if (HasSkill(idx))
        {
            Destroy(_skillDict[idx].gameObject);
            _skillDict.Remove(idx);
            return true;
        }
        return false;
    }

    private bool HasSkill(int idx)
    {
        if (!_skillDict.ContainsKey(idx))
            return false;
        else return true;
    }

    /// <summary>
    /// 스킬 전부 여기서 업데이트 하게 함
    /// </summary>
    private void Update()
    {
        //for - _skillDict - update
    }
}
