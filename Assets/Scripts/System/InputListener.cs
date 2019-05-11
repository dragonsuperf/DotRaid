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
    private List<Character> _charactors;

    private eSkill _skill;

    //----------
    [SerializeField] private UIHelper _uiHelper;
    public bool isSelecting = false; //인스펙터 표기용
    public List<Character> selectedUnits = new List<Character>(); //선택된 캐릭터 인스펙터 표기용
    public List<Character> selectTemp = new List<Character>(); //아무것도 선택안했을 때 선택된 캐릭터 남기기용
    
    private Vector3 mousePosition;

    private void Start()
    {
        _charactors = GameManager.Instance.GetChars();
    }

    void Update()
    {
        SelectCharactorWithBound();
        SelectCharactor();
        SkillInput();

       
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            var rect = _uiHelper.GetSelectRect(mousePosition, Input.mousePosition);
            _uiHelper.DrawSelectRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            _uiHelper.DrawSelectRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    private void SelectCharactorWithBound()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            mousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isSelecting)
            {
                
                IsWithinSelectionBounds();

                if(selectTemp.Count != 0) // 무언가 select 했을때
                {
                    for (int i = 0; i < selectedUnits.Count; i++)
                    {
                        selectedUnits[i].stat.isSelect = false;
                    }
                    selectedUnits.Clear();
                    selectedUnits = new List<Character>(selectTemp);
                    for (int i = 0; i < selectedUnits.Count; i++)
                    {
                        selectedUnits[i].stat.isSelect = true;
                    }
                    selectTemp.Clear();
                }

                else // select 안했을때 selectedUnits 그대로 가져감
                {

                }
            }
            isSelecting = false;
        }
    }

    /// <summary>
    /// 캐릭터 고름 1~5번으루
    /// </summary>
    private void SelectCharactor()
    {
        if (_state == InputState.None)
        {
            int preIdx = _selectCharactorIdx;
            //1~5번으로 캐릭터 고름
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedUnits.Clear();
                _selectCharactorIdx = 0;
                selectedUnits.Add(_charactors[0]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedUnits.Clear();
                _selectCharactorIdx = 1;
                selectedUnits.Add(_charactors[1]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selectedUnits.Clear();
                _selectCharactorIdx = 2;
                selectedUnits.Add(_charactors[2]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                selectedUnits.Clear();
                _selectCharactorIdx = 3;
                selectedUnits.Add(_charactors[3]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                selectedUnits.Clear();
                _selectCharactorIdx = 4;
                selectedUnits.Add(_charactors[4]);
            }

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
    /// <summary>
    /// 사각형 안에 있는거 확인?
    /// </summary>
    public void IsWithinSelectionBounds()
    {
        var camera = Camera.main;
        var viewportBounds =
            _uiHelper.GetViewportBounds(camera, mousePosition, Input.mousePosition);

        for (int i = 0; i < _charactors.Count; i++)
        {
            if (viewportBounds.Contains(camera.WorldToViewportPoint(_charactors[i].transform.position)))
            {
                selectTemp.Add(_charactors[i]);
            }
        }
    }
}
