using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillClick : MonoBehaviour
{
    GameManager gameManager;
    SkillManager skillManager;
    Character defencer;
    GameObject[] characters;
    Character[] usingChar;
    CharacterStats[] usingCharInitStat;
    int[] percentHp;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();

        defencer = GameObject.Find("Defencer").GetComponent<Character>();

        characters = gameManager.GetChars();
        usingChar = new Character[characters.Length];
        percentHp = new int[characters.Length];
        usingCharInitStat = new CharacterStats[characters.Length];

        for (int i = 0; i < characters.Length; i++)
        {
            usingChar[i] = characters[i].GetComponent<Character>();
            usingCharInitStat[i] = usingChar[i].stat;
        }
    }


    // Update is called once per frame
    void Update()
    {
      

    }

    /*
    public void DefencerSkillOne()
    {
        StartCoroutine(skillManager.DefUp(defencer));
    }

    public void Regeneration()
    {
        int count = 0;
        StartCoroutine(skillManager.Regeneration(usingChar,usingCharInitStat,count));
    }

    public void MinorHeal()
    {
        StartCoroutine(skillManager.MinorHeal());
    }
    */
    /*
    void FindLowestHp()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            percentHp[i] = usingChar[i].stat.hp / usingCharInitStat[i].hp * 100;
        }
        Array.Sort(percentHp);
    }
    */
}
