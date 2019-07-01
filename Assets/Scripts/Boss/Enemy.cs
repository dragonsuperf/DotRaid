using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

[Serializable]
public struct EnemyStats
{
    public int hp;
    public int attackRangeRadius;
    public int awareRangeRadius;
    public float moveSpeed;
    public int attackSpeed;
    public int CastSpeed;
    public int phisicDamage;
    public int magicDamage;
}

public enum EnemyState
{
    idle, attack, move, cast, chase, count
}

public class Enemy : Actor
{
    //    public EnemyStats stat;
    public PatternManager pm;

    GameManager gameManager;
    CircleCollider2D attackRangeCollider;

    protected GameObject forceTarget; // currentTaget보다 우선시되는 타겟
    protected EffectManager em;
    Animator ani;
    List<Character> characters;
    bool isLookLeft = true;
    bool isUnable;

    protected float fullHP;
    float attackDelay = 0.0f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        attackRangeCollider = Util.GetChildWithName(gameObject, "AttackRange").gameObject.GetComponent<CircleCollider2D>();
        Debug.Log(attackRangeCollider);
        ani = GetComponent<Animator>();
        pm.SetAnimator(ani);
        gameManager = GameManager.Instance;
        em = EffectManager.Instance ;
        characters = gameManager.GetChars();
        currentTarget = GetClosest(); // 첫번째 공격타겟 
        fullHP = stat.hp;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (stat.hp <= 0)
        {
            pm.Pause();
            Dead();
            return;
        }

        FindTarget(); // 타겟 찾기
        JudgeAndDoAction(); // 행동 결정 및 실행

        attackDelay += Time.deltaTime;
    }

    void JudgeAndDoAction()
    {
        if (currentTarget == null) return;
        if (isLookLeft && transform.position.x < (forceTarget != null ? forceTarget.transform.position.x : currentTarget.transform.position.x))
        {
            isLookLeft = false;
            transform.Rotate(0, 180, 0);
        }
        else if (!isLookLeft && transform.position.x > (forceTarget != null ? forceTarget.transform.position.x : currentTarget.transform.position.x))
        {
            isLookLeft = true;
            transform.Rotate(0, 180, 0);
        }

        if (pm.IsRunning()) return; // 패턴이 실행 중이면 기본 동작 스킵


        if (state == ActorState.chase)
        {
            if (!pm.IsRunning())
                MoveToPosition(currentTarget.position);
        }
        else if (state == ActorState.idle)
        {

        }
    }

    void FindTarget()
    {
        if (forceTarget != null) return; // 우선 타겟이 있을 경우 적을 탐색하지 않음
        Transform closest = GetClosest();
        currentTarget = closest;
        if (currentTarget == null) return;

        if (attackRangeCollider.OverlapPoint(currentTarget.position)) // 현재 타겟이 사거리 내에 있다면 타겟 변경 없음
        {
            state = ActorState.idle;
            return;
        }

        if (!attackRangeCollider.OverlapPoint(closest.position)) // 가장 가까운 타겟이 사거리 내에 없다면 추적 상태로 변경
        {
            state = ActorState.chase;
        }
    }

    protected GameObject GetRandomTarget()
    {
        GameObject obj = null;
        for(int i = 0; i < 50; i++) // limit 50
        {
            obj = characters[UnityEngine.Random.Range(0, characters.Count)].gameObject;
            if (obj.GetComponent<Actor>().CharState != ActorState.dead)
                break;
            else
                obj = null;
        }
        return obj;
    }

    protected void MoveToPosition(Vector2 position)
    {
        transform.position = Vector2.MoveTowards(transform.position, position, stat.moveSpeed * Time.deltaTime);
    }

    void CheckAttackable() // 평타 로직
    {
        if (state != ActorState.idle)
            pm.SkipCurrentPattern();
    }

    Transform GetClosest() // 가장 가까운 캐릭터를 찾음
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].CharState == ActorState.dead) continue;

            float dist = Vector3.Distance(characters[i].transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = characters[i].transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    void AttackEnd()
    {
        if (currentTarget != null)
            em.PlayEffectOnPosition("blast", currentTarget.transform.position, 1.0f);
    }

    void Dead()
    {  
        enabled = false;
        StartCoroutine(em.DestroyWithFadeOut(gameObject));

        // broadcast i'm dead
        DungeonManager.Instance.RemoveEnemyInRoom(DungeonManager.Instance.GetCurrentDungeonRoom(), this);
    }

    public IEnumerator DebugDead(){
        yield return new WaitForSeconds(0.5f);
        Dead();
    }
}
