using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarrior : Enemy
{
    // Start is called before the first frame update
    Vector2 chargePoint;
    public Effect afterimage;

    protected override void Start()
    {
        base.Start();

        pm.AddPattern("attack", 0.3f, 3.0f, 2.0f);
        pm.AddPattern("charge", 0.8f, 10.0f, 1.0f);

        em.AddEffectToPool("knightAfterimage", afterimage);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (forceTarget != null) // forceTarget이 있을경우 돌진
            MoveToPosition(chargePoint);

        base.Update();
    }

    void TargetCharge() // 돌진 시작
    {
        forceTarget = GetRandomTarget();
        chargePoint = new Vector2(forceTarget.transform.position.x, forceTarget.transform.position.y); //돌진 대상

        stat.moveSpeed *= 10.0f;

        GameUtil.GetChildWithName(gameObject, "Charge").gameObject.SetActive(true);

        StartCoroutine(MakeSomeAfterimage());
    }

    void TargetChargeEnd() // 돌진 끝
    {
        forceTarget = null;
        stat.moveSpeed /= 10.0f;

        GameUtil.GetChildWithName(gameObject, "Charge").gameObject.SetActive(false);
    }

    IEnumerator MakeSomeAfterimage()
    {
        for (int i = 0; i < 5; i++)
        {
            em.PlayEffectOnPosition("knightAfterimage", transform.position, 0.1f, transform.localRotation);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
