using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Unit : MonoBehaviour
{

    public int alliance;

    protected float currentHp;
    [HideInInspector] public float currentAttackCooldown;

    public float baseMaxHp = 20f;
    public float baseDamage = 1f;
    public float baseAttackSpeed = 1f;
    public float baseSpeed = 1f;

    public float baseHpRegen = (150f / 80f);

    [HideInInspector] public float maxHp;
    [HideInInspector] public float damage;
    [HideInInspector] public float speed;
    [HideInInspector] public float attackSpeed;

    public float predictDamage; // 미사일에 타겟이 되어 확정적으로 데미지를 받고 남을 hp
    public bool PredictDead => currentHp <= predictDamage;

    public bool Dead => currentHp <= 0f;

    public SpriteRenderer sprite;
    private Vector3 baseScale;
    private Material baseMaterial;

    public Gauge hpGauge;

    // 공용 라이브러리
    public Material attackedMaterial;

    public static EventHandler<bool> dieEvent;

    protected virtual void Awake()
    {
        currentHp = maxHp;

        baseScale = sprite.transform.localScale;
        baseMaterial = sprite.material;

        maxHp = baseMaxHp;
        damage = baseDamage;
        speed = baseSpeed;
        attackSpeed = baseAttackSpeed;

        if (hpGauge != null)
            hpGauge.Show();
    }

    public void Die()
    {
        gameObject.SetActive(false);

        sprite.material = baseMaterial;
        sprite.transform.localScale = baseScale;

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
        if (Dead) return;

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

        if (hpGauge != null)
            hpGauge.SetGauge(currentHp, maxHp);
    }

    public void Damage(float dam)
    {
        if (Dead) return;

        //AddHp(-1f * dam);
        DamageAnimation(-1f * dam);
    }

    protected void DamageAnimation(float dam)
    {
        //sprite.transform.DORestart();
        sprite.transform.DOKill(true);


        sprite.material = attackedMaterial;
        sprite.transform.localScale = baseScale;

        sprite.transform.DOPunchScale(new Vector3(-0.45f, 0.5f, 0f), 0.1f, 10, 0.1f).SetUpdate(false).SetEase(Ease.InCubic).OnComplete(() =>
        {
            sprite.transform.DOPunchScale(new Vector3(0.5f, -0.45f, 0f), 0.1f, 10, 0.1f).SetUpdate(false).SetEase(Ease.InCirc).OnComplete(() =>
            {
                sprite.material = baseMaterial;
                AddHp(dam);
            });
        });

    }
}
