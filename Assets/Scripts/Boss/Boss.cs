using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Boss : Enemy
{
    public GameObject[] mobPrefab;

    [SerializeField] private Image _hpBar;

    public Effect afterImage;
    public Projectile proj;

    Vector2[] spawnPoint = { new Vector2(-13.0f, 6.0f), new Vector2(13.0f, 6.0f) };
    Vector2 chargePoint; // 돌진 대상 위치

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        pm.AddPattern("attack", 0.3f, 3.0f, 1.0f);
        pm.AddPattern("cast", 2.0f, 10.0f, 0.5f);
        pm.AddPattern("charge", 1.2f, 8.0f, 0.5f);
        pm.AddPattern("shootcast", 2.0f, 8.0f, 0.5f);

        em.AddEffectToPool("bossAfterimage", afterImage);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (forceTarget != null)
            MoveToPosition(chargePoint);

        //_hpBar.fillAmount = stat.hp / fullHP;

        base.Update();
    }

    void ThrowFiveProjectile()
    {
        for(int i = 0; i < 5; i++)
        {
            Projectile p = Instantiate(proj);
            p.Set(10.0f, 300.0f, transform.position, currentTarget.position, (-30.0f + (15.0f * i)));
        }
    }

    void SummonSomething() // cast 패턴시 시전되는 소환 스킬
    {
        Instantiate(mobPrefab[UnityEngine.Random.Range(0, 2)], spawnPoint[UnityEngine.Random.Range(0, 2)], Quaternion.identity);
    }

    void TargetCharge() // 돌진 시작
    {
        forceTarget = GetRandomTarget();
        chargePoint = new Vector2(forceTarget.transform.position.x, forceTarget.transform.position.y); //돌진 대상
        
        stat.moveSpeed *= 5.0f;

        StartCoroutine(MakeSomeAfterimage());
    }

    void TargetChargeEnd() // 돌진 끝
    {
        forceTarget = null;
        stat.moveSpeed /= 5.0f;
    }

    IEnumerator MakeSomeAfterimage()
    {
        for (int i = 0; i < 5; i++)
        {
            em.PlayEffectOnPosition("bossAfterimage", transform.position, 0.1f, transform.localRotation);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
