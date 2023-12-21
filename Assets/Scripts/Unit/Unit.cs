using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{

    public int alliance;

    public float maxHp = 10f;
    public float currentHp;

    public bool Dead => currentHp <= 0f;

    public static EventHandler<bool> dieEvent;

    protected virtual void Awake()
    {
        currentHp = maxHp;
    }

    public void Die()
    {
        gameObject.SetActive(false);

        if (dieEvent != null)
            dieEvent(this, true);
    }

    public void SetHp(float value)
    {
        currentHp = value;
        HandleHp();
    }

    public void AddHp(float value)
    {
        currentHp += value;
        HandleHp();
    }

    private void HandleHp()
    {
        if (currentHp <= 0f)
        {
            Die();
        }
        else
        {
            if (currentHp > maxHp)
            {
                currentHp = maxHp;
            }
        }
    }

    public void Damage(float dam)
    {
        if (Dead) return;

        AddHp(-1f * dam);
    }

}
