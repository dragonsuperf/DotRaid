using Pathfinding;
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
    idle, attack, move, cast, chase, count
}

public class Actor : MonoBehaviour
{
    protected Transform currentTarget;
    [SerializeField]
    protected ActorState state;
    [SerializeField]
    protected ActorStats stat;
    public ActorStats Stat { get { return stat; } private set { } }
    protected Vector3 curMovePosition;
    
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
    
    public void TakeDamage(float damage)
    {
        stat.hp -= damage;
    }

    public void TakeDamage(float damage, DamageType type)
    {
        stat.hp -= damage - (type == DamageType.physic ? stat.physicDef : stat.magicDef);
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

    public void SetDestinationAstar(Transform destination) // Astar pathfinding 목적지 설정 : 게임오브젝트의 Transform 넣으면 됨
    {
        this.GetComponent<AIDestinationSetter>().target = destination;
    }

    public void SetDestinationAstar(Transform destination,float speed) // Speed
    {
        this.GetComponent<AIDestinationSetter>().target = destination;
        this.GetComponent<AIPath>().maxSpeed = speed;
    }
}
