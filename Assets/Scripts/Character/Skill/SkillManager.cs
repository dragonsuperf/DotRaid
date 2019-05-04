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
    private GameManager _gameManager;
    private int _skillCount = 0;

    private GameObject _skillRoot = null;

    protected override void Start()
    {
        base.Start();
        _skillRoot = new GameObject("SkillRoot");
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// 스킬 생성함 프리팹이름이 스킬클래스 이름이어야함
    /// </summary>
    /// <typeparam name="T">스킬 객체만 ㅇㅋ</typeparam>
    public void Create<T>(SkillData skillData) where T : Skill
    {
        if (skillData == null) return;
        Type type = typeof(T);
        T loadPrefab = null;
        loadPrefab = Resources.Load<T>("Skill/" + type.ToString());
        if (loadPrefab == null)
        {
            Debug.Log("스킬 안만들어짐");
            return;
        }
        Skill newSkill = Instantiate<T>(loadPrefab, _skillRoot.transform).GetComponent<Skill>();
        newSkill.IDX = _skillCount;
        _skillDict.Add(_skillCount, newSkill);

        newSkill.OnSet(skillData);
        newSkill.Data.createEffectCallback.SafeInvoke();
        Debug.Log("스킬 생성 " + type.ToString() + " 스킬번호 : " + _skillCount + " " + skillData.player_idx);
        _skillCount++; // TODO 키 유니크하게 관리해야함
    }

    public bool Remove(int idx)
    {
        if (HasSkill(idx))
        {
            _skillDict[idx].OnRemove();
            _skillDict[idx].Data.hitEffectCallback.SafeInvoke();
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
