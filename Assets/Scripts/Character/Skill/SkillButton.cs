using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    //TODO 클래스 마다 어떤 스킬을 가지고 있는지 스킬 정보가 필요함
    public void OnClick()
    {
        SkillManager.Instance.Create<TestSkill>();
    }
}
