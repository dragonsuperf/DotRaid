using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputState
{
    None = 0,
    SkillWait,
}

public class InputListener : Singleton<InputListener>
{
    private InputState _state = InputState.None;
    private int _charactorIdx = 0;

    protected override void Start()
    {
        base.Start();
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
            int preIdx = _charactorIdx;
            //1~5번으로 캐릭터 고름
            if (Input.GetKeyDown(KeyCode.Alpha1)) _charactorIdx = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2)) _charactorIdx = 1;
            if (Input.GetKeyDown(KeyCode.Alpha3)) _charactorIdx = 2;
            if (Input.GetKeyDown(KeyCode.Alpha4)) _charactorIdx = 3;
            if (Input.GetKeyDown(KeyCode.Alpha5)) _charactorIdx = 4;
            if (preIdx != _charactorIdx) Debug.Log(_charactorIdx + " 바뀜 ");
        }
    }

    private void SkillInput()
    {
        if (_state == InputState.None)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _state = InputState.SkillWait;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _state = InputState.SkillWait;
            }
        }
        if (_state == InputState.SkillWait)
        {
            //타겟 지정
            SkillData info = new SkillData();
            info.player_idx = _charactorIdx;
            //플레이어 한테 스킬 정보를 받아서 그 스킬을 생성해야 함
            SkillManager.Instance.Create<TestSkill>(info);
            _state = InputState.None;
        }
    }
}
