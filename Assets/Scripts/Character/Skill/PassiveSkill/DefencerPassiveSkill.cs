using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 디펜서 - 페시브
/// 주변 아군 받는 피해 감소
/// </summary>
[SkillJobKind(SkillJobKindAttribute.eAttribute.Tanker)]
public class DefencerPassiveSkill : PassiveSkillBase
{
    public bool isDefencerBuff = false;
    private float _limitDistance = 0f;
    private int _defencerIDX = -1;
    List<Character> _chracters;

    Dictionary<int, bool> _effectIDXs = new Dictionary<int, bool>();

    float startTime;
    float tick = 1.0f;

    /// <summary>
    /// 생성 타이밍에 꼭 부르세요 !
    /// </summary>
    /// <param name="limitDistance">버프 영향 거리</param>
    public void Init(float limitDistance)
    {
        _limitDistance = limitDistance;
    }

    public override void OnSet(object data = null)
    {
        base.OnSet(data);
        isDefencerBuff = true;

        startTime = Time.time;
        _chracters = GameManager.Instance.GetChars();
        _defencerIDX = _data.player_info.idx;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (startTime + tick > Time.time)
            return;

        foreach (var character in _chracters)
        {
            if (character.IDX == _defencerIDX)
                continue;
            if (CheckDistance( character.transform.position, _limitDistance))
            {
                if (_effectIDXs.ContainsKey(character.IDX))
                    continue;

                _effectIDXs.Add(character.IDX, true);
                character.CharPhysicDef += 5;

                Debug.Log("영향받은 캐릭터 " + character.IDX);
            }
            else
            {
                if (!_effectIDXs.ContainsKey(character.IDX))
                    continue;

                _effectIDXs.Remove(character.IDX);
                character.CharPhysicDef -= 5;

                Debug.Log("영향 사라진 캐릭터 " + character.IDX);
            }
        }
        startTime = Time.time;
    }

    public override void OnRemove()
    {
        isDefencerBuff = false;
        base.OnRemove();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_chracters[_defencerIDX].transform.position, _limitDistance);
    }

    /// <summary>
    /// 거리조건
    /// </summary>
    /// <returns>나랑 dist보다 적은 거리면 true리턴</returns>
    private bool CheckDistance(Vector2 otherPos, float dist)
    {
        if (Vector2.Distance(_chracters[_defencerIDX].transform.position, otherPos) < dist)
            return true;
        return false;
    }
}
