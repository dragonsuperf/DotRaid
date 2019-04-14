using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] chracters;
    public GameObject boss;
    public EffectManager effectManager;
    public Effect defaultBlastEffect;

    // Start is called before the first frame update
    void Start()
    {
        effectManager.AddEffectToPool("blast", defaultBlastEffect, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject[] GetChars() => chracters;
    public GameObject GetBoss() => boss;
}
