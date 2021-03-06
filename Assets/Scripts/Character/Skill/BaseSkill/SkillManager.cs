﻿using System.Collections;
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
    /// 스킬 루트 속성 리플렉션
    /// </summary>
    /// <returns>루트string</returns>
    private string GetSkillJobKindAttribute<T>()
    {
        Type type = typeof(T);
        string root = string.Empty;
        SkillJobKindAttribute[] attrs = (SkillJobKindAttribute[])type.GetCustomAttributes(typeof(SkillJobKindAttribute), true);  
        foreach (SkillJobKindAttribute attr in attrs)
        {
            SkillJobKindAttribute a = attr;
            root = a.folderRoot.ToString();
        }
        return root;
    }

    /// <summary>
    /// 각 캐릭터에서 패시브 생성
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public Skill CreatePassive<T>(SkillData skillData)
    {
        if(skillData==null) return null;
        Type type = typeof(T);
        GameObject newObj = Instantiate(new GameObject(type.ToString()),_skillRoot.transform);
        Skill newSkill = newObj.AddComponent(type) as Skill;
        newSkill.IDX = _skillCount;
        _skillDict.Add(_skillCount, newSkill);

        newSkill.OnSet(skillData);

        _skillCount++; // TODO 키 유니크하게 관리해야함
        return newSkill;
    }

    /// <summary>
    /// 스킬 생성함 프리팹이름이 스킬클래스 이름이어야함
    /// </summary>
    /// <typeparam name="T">스킬 객체만 ㅇㅋ</typeparam>
    public void Create(eSkill skill, SkillData skillData)
    {
        if (skillData == null) return;
        GameObject loadPrefab = Resources.Load<GameObject>("Prefabs/Skill/" + skill.ToString());
        if (loadPrefab == null)
        {
            Debug.Log("스킬 안만들어짐");
            return;
        }
        Skill newSkill = Instantiate(loadPrefab, _skillRoot.transform).GetComponent<Skill>();
        newSkill.IDX = _skillCount;
        _skillDict.Add(_skillCount, newSkill);

        newSkill.OnSet(skillData);
        newSkill.Data.createEffectCallback.SafeInvoke();
        Debug.Log("스킬 생성 " + skill.ToString() + " 스킬번호 : " + _skillCount + "/ 플레이어 번호 :" + skillData.player_info.idx);
        _skillCount++; // TODO 키 유니크하게 관리해야함
    }

    public bool Remove(int idx)
    {
        if (HasSkill(idx))
        {
            Debug.Log("스킬번호 지워짐 : " + idx);
            _skillDict[idx].OnRemove();
            _skillDict[idx].Data.hitEffectCallback.SafeInvoke();
            Destroy(_skillDict[idx].gameObject);
            _skillDict.Remove(idx);
            return true;
        }
        return false;
    }

    public bool HasSkill(int idx)
    {
        if (!_skillDict.ContainsKey(idx))
            return false;
        else return true;
    }

    private List<int> _keyList;
    /// <summary>
    /// 스킬 전부 여기서 업데이트 하게 함
    /// </summary>
    private void Update()
    {
        _keyList = new List<int>(_skillDict.Keys);

        for (int i = 0; i< _keyList.Count; ++i)
        {
            _skillDict[_keyList[i]].OnUpdate();
        }
    }
}
