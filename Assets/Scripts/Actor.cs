﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ActorStats
{
    public float hp;
    public float attackRangeRadius;
    public float awareRangeRadius;
    public float moveSpeed;
    public float attackSpeed;
    public float CastSpeed;
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
    protected ActorStats stat;
    protected Vector3 curMovePosition;

    //public ActorStats Stat { get => stat; set => stat = value; }

    public void TakeDamage(float damage)
    {
        stat.hp -= damage;
    }

    public void TakeDamage(float damage, DamageType type)
    {
        stat.hp -= damage - (type == DamageType.physic ? stat.physicDef : stat.magicDef);
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
}