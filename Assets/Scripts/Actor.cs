using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ActorStats
{
    public bool isSelect;
    public float hp;
    public float maxHp;
    public float attackRangeRadius;
    public float awareRangeRadius;
    public float moveSpeed;
    public float attackSpeed;
    public float castSpeed;
    public float physicDamage;
    public float magicDamage;
    public float physicDef;
    public float magicDef;
    
}

public enum DamageType
{
    magic, physic, count
}

public enum ActorState
{
    idle, attack, move, cast, chase, count, dead
}

public class Actor : MonoBehaviour
{
    protected Transform currentTarget;
    [SerializeField]
    protected ActorState state;
    [SerializeField]
    protected ActorStats stat;
    public ActorStats Stat { get { return stat; } private set { } }
    protected Vector3 curMovePosition; //마우스로 찍은 부분

    protected int _idx = 0; //캐릭터 고유 index (게임매니저의 인덱스랑 싱크가 맞아야 함)
    public int IDX { get { return _idx; } private set { } }
    public void SetIDX(int val) { _idx = val; }

    public Transform CurrentTarget { get { return currentTarget; } private set { } }
    public bool CharSelect { get => stat.isSelect; set => stat.isSelect = value; }
    public float HP { get => stat.hp; set => stat.hp = value; }
    public float CharAttackRangeRadius { get => stat.attackRangeRadius; set => stat.attackRangeRadius = value; }
    public float CharAwareRangeRadius { get => stat.awareRangeRadius; set => stat.awareRangeRadius = value; }
    public float CharMoveSpeed { get => stat.moveSpeed; set => stat.moveSpeed = value; }
    public float CharCastSpeed { get => stat.castSpeed; set => stat.castSpeed = value; }
    public float CharPhysicDamage { get => stat.physicDamage; set => stat.physicDamage = value; }
    public float CharMagicDamage { get => stat.magicDamage; set => stat.magicDamage = value; }
    public float CharPhysicDef { get => stat.physicDef; set => stat.physicDef = value; }
    public float CharMagicDef { get => stat.magicDef; set => stat.magicDef = value; }
    public ActorState CharState { get => state; set => state = value; }

    public void TakeDamage(float damage)
    {
        stat.hp -= damage;
        //Debug.Log(this.name + " take damange : " + damage + " left health is : " + stat.hp);
    }

    public void TakeDamage(float damage, DamageType type)
    {
        stat.hp -= damage - (type == DamageType.physic ? stat.physicDef : stat.magicDef);
    }

    public void StopDotCorotine()
    {
        StopCoroutine("StartTakeDotDamage");
    } 
    public void StartDotCorotine(float tickDamage, float tickTime, float duringTime, DamageType type)
    {
        StartCoroutine(StartTakeDotDamage(1, 0.5f, 15f, DamageType.physic));
    }
    public IEnumerator StartTakeDotDamage(float tickDamage, float tickTime, float duringTime, DamageType type)
    {
        float StartTime = Time.time;
        while (true)
        {
            if (StartTime + duringTime < Time.time)
                yield break;
            if (StartTime + tickTime < Time.time)
            {
                Debug.Log("보스에게 도트딜 " + tickDamage);
                TakeDamage(tickDamage, type);
                StartTime = Time.time;
            }
            yield return null;
        }
    }

    public void TakeHeal(float heal)
    {
        if (heal <= 0) return;
        float healHp = stat.hp + heal;
        stat.hp = (healHp > stat.maxHp) ? stat.maxHp : healHp;
    }

    public void CharFlipping()
    {
        if (currentTarget)
        {
            if (currentTarget.position.x < this.transform.position.x)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    /// <summary>
    /// 타겟이 뒤에[있냐 압에 있냐??
    /// </summary>
    public bool GetActorBack()
    {
        bool IsTgLeft = false;
        bool IsLeft = false;

        if (currentTarget.rotation.y > 179 && currentTarget.rotation.y < 181) IsTgLeft = true;
        else IsTgLeft = false;

        if (transform.rotation.y > 179 && transform.rotation.y < 181) IsLeft = true;
        else IsLeft = false;

        if (IsTgLeft == IsLeft) return true;
        else return false;
    }

}
