﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.SceneManagement;

public enum InputState
{
    None = 0,
    SkillWait,
}

public class InputListener : MonoBehaviour
{
    private InputState _state = InputState.None;
    private int _selectCharactorIdx = 0;
    private List<Character> _characters;

    private eSkill _skill;
    private eSkillState _skillState;

    //----------
    [SerializeField] private UIHelper _uiHelper;
    public bool isSelecting = false; //인스펙터 표기용
    public List<Character> selectedUnits = new List<Character>(); //선택된 캐릭터 인스펙터 표기용
    public List<Character> selectTemp = new List<Character>(); //아무것도 선택안했을 때 선택된 캐릭터 남기기용
    
    private Vector3 mousePosition;
    private Camera _mainCamera;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name != "Lobby"){
            _characters = GameManager.Instance.GetChars();
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        
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

                if(selectTemp.Count > 1) // 무언가 select 했을때
                {
                    for (int i = 0; i < selectedUnits.Count; i++) //모두선택 해제 (발밑 원모양 표시위하여, isSelect false)
                    {
                        selectedUnits[i].CharSelect = false; 
                    }
                    selectedUnits.Clear();
                    selectedUnits = new List<Character>(selectTemp);
                    for (int i = 0; i < selectedUnits.Count; i++) //선택된것들 선택
                    {
                        selectedUnits[i].CharSelect = true;
                    }
                    selectTemp.Clear();                 
                }

                else if(selectTemp.Count != 0) // only one selected
                {
                    for (int i = 0; i < selectedUnits.Count; i++) //모두선택 해제 (발밑 원모양 표시위하여, isSelect false)
                    {
                        selectedUnits[i].CharSelect = false;
                    }
                    selectedUnits.Clear();
                    selectedUnits = new List<Character>(selectTemp);
                    for (int i = 0; i < _characters.Count; i++)
                    {
                        if (selectedUnits[0] == _characters[i])
                        {
                            _selectCharactorIdx = i;
                        }
                    }
                    selectedUnits[0].CharSelect = true;
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
                for (int i = 0; i < selectedUnits.Count; i++) //모두선택 해제 (발밑 원모양 표시위하여, isSelect false)
                {
                    selectedUnits[i].CharSelect = false;
                }
                selectedUnits.Clear();
                _selectCharactorIdx = 0;
                selectedUnits.Add(_characters[0]);
                selectedUnits[0].CharSelect = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                for (int i = 0; i < selectedUnits.Count; i++) //모두선택 해제 (발밑 원모양 표시위하여, isSelect false)
                {
                    selectedUnits[i].CharSelect = false;
                }
                selectedUnits.Clear();
                _selectCharactorIdx = 1;
                selectedUnits.Add(_characters[1]);
                selectedUnits[0].CharSelect = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                for (int i = 0; i < selectedUnits.Count; i++) //모두선택 해제 (발밑 원모양 표시위하여, isSelect false)
                {
                    selectedUnits[i].CharSelect = false;
                }
                selectedUnits.Clear();
                _selectCharactorIdx = 2;
                selectedUnits.Add(_characters[2]);
                selectedUnits[0].CharSelect = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                for (int i = 0; i < selectedUnits.Count; i++) //모두선택 해제 (발밑 원모양 표시위하여, isSelect false)
                {
                    selectedUnits[i].CharSelect = false;
                }
                selectedUnits.Clear();
                _selectCharactorIdx = 3;
                selectedUnits.Add(_characters[3]);
                selectedUnits[0].CharSelect = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                for (int i = 0; i < selectedUnits.Count; i++) //모두선택 해제 (발밑 원모양 표시위하여, isSelect false)
                {
                    selectedUnits[i].CharSelect = false;
                }
                selectedUnits.Clear();
                _selectCharactorIdx = 4;
                selectedUnits.Add(_characters[4]);
                selectedUnits[0].CharSelect = true;
            }

            if (preIdx != _selectCharactorIdx) Debug.Log(_selectCharactorIdx + " 바뀜 ");
        }
    }

    private void SkillInput()
    {
        //스킬 버튼 누를 때
        if (_state == InputState.None)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _state = InputState.SkillWait;
                _skill = _characters[_selectCharactorIdx].skill_first;
                Debug.Log("선택스킬 : " + _skill);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _state = InputState.SkillWait;
                _skill = _characters[_selectCharactorIdx].skill_second;
                Debug.Log("선택스킬 : " + _skill);
            }
        }
        //스킬 누르고 타겟 누를 때
        if (_state == InputState.SkillWait)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _skillState = CheckSkillState(_skill);
                _characters[_selectCharactorIdx].skillStateData.skillState = _skillState;
                _characters[_selectCharactorIdx].skillStateData.skillMakeCallback = MakeSkill;
                _state = InputState.None;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _characters[_selectCharactorIdx].skillStateData.Clear();
                _state = InputState.None;
            }
        }
    }
    
    private SkillData MakeSkill()
    {
        eTargetState targetState = eTargetState.Enemy;
        // 형식에따라 마우스로 타겟을 어디를 지정할건지 정하는거임
        if(_skillState == (eSkillState.NonTarget_Cast) )
        {
            targetState = eTargetState.Ground;
        }

        SkillData info = new SkillData();
        info.player_info = new CasterInfo(); //시전자 정보임
        info.player_info.idx = _selectCharactorIdx;
        info.player_info.pos = _characters[_selectCharactorIdx].transform.position;
        
        info.target_info = new TargetInfo(); //타겟 정보임
        info.target_info.pos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        info.target_info.state = targetState;

        //플레이어 한테 스킬 정보를 받아서 그 스킬을 생성해야 함
        SkillManager.Instance.Create(_skill, info);
        _state = InputState.None;
        return info; 
    }

    /// <summary>
    /// 데이터가 없으므로 스킬종류 수동으로 구별
    /// </summary>
    /// <returns></returns>
    private eSkillState CheckSkillState(eSkill skill)
    {
        eSkillState state = eSkillState.Nomal;
        switch(skill)
        {
            case eSkill.SinglePoisonSkill:
                state = eSkillState.JustMake;
                break;
            case eSkill.Sniping:
                state = eSkillState.NonTarget_Cast;
                break;
            default:
                state = eSkillState.NonTarget_Cast;// state = eSkillState.Nomal;
                break;
        }
        return state;
    }

    /// <summary>
    /// 사각형 안에 있는거 확인?
    /// </summary>
    public void IsWithinSelectionBounds()
    {
        var camera = Camera.main;
        var viewportBounds =
            _uiHelper.GetViewportBounds(camera, mousePosition, Input.mousePosition);
        Vector2 worldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        //if(mousePosition == Input.mousePosition) // if Click
        if(Vector2.Distance(mousePosition, Input.mousePosition)<1)   
        {
            if (hit.collider)
            {
                //selectTemp.Add(hit.transform.GetComponent<Character>());
                
                for (int i = 0; i < _characters.Count; i++)
                {
                    if (hit.transform.GetComponent<Character>() == _characters[i])
                    {
                        _selectCharactorIdx = i;
                        selectTemp.Add(_characters[i]);
                    }
                }
                
            }
        }
        else // if Drag
        {
            for (int i = 0; i < _characters.Count; i++)
            {
                if (viewportBounds.Contains(camera.WorldToViewportPoint(_characters[i].transform.position)))
                {
                    selectTemp.Add(_characters[i]);
                }
            }
        }
        
    }
}
