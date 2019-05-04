using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Character[] chracters;
    public GameObject boss;
    public EffectManager effectManager;
    public Effect defaultBlastEffect;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        effectManager.AddEffectToPool("blast", defaultBlastEffect, 10);
        SkillManager.Instance.OnSet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Character[] GetChars() => chracters;
    public GameObject GetBoss() => boss;
}
