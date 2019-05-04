using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public enum InputState
{
    None = 0,
    SkillWait,
}

public class InputListener : MonoBehaviour
{
    private InputState _state = InputState.None;
    private int _selectCharactorIdx = 0;
    private Character[] _charactors;

    private eSkill _skill;

    private void Start()
    {
        _charactors = GameManager.Instance.GetChars();
    }

    void Update()
    {
        SelectCharactor();
        SkillInput();
    }

    private void SelectCharactor()
    {
        if (_state == InputState.None)
        {
            int preIdx = _selectCharactorIdx;
            //1~5번으로 캐릭터 고름
            if (Input.GetKeyDown(KeyCode.Alpha1)) _selectCharactorIdx = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2)) _selectCharactorIdx = 1;
            if (Input.GetKeyDown(KeyCode.Alpha3)) _selectCharactorIdx = 2;
            if (Input.GetKeyDown(KeyCode.Alpha4)) _selectCharactorIdx = 3;
            if (Input.GetKeyDown(KeyCode.Alpha5)) _selectCharactorIdx = 4;
            if (preIdx != _selectCharactorIdx) Debug.Log(_selectCharactorIdx + " 바뀜 ");
        }
    }

    private void SkillInput()
    {
        if (_state == InputState.None)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _state = InputState.SkillWait;
                _skill = _charactors[_selectCharactorIdx].skill_first;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _state = InputState.SkillWait;
                _skill = _charactors[_selectCharactorIdx].skill_second;
            }
        }
        if (_state == InputState.SkillWait)
        {
            //TODO 타겟 지정.. 타겟은 클릭한 목표를 의미
            SkillData info = new SkillData();
            info.player_idx = _selectCharactorIdx;

            //플레이어 한테 스킬 정보를 받아서 그 스킬을 생성해야 함
            SkillManager.Instance.Create(_skill, info);
            _state = InputState.None;
        }
    }
}
