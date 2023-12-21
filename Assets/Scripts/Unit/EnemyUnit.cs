using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{

    public Unit target;

    public Rigidbody2D rg;

    public float attackSpeed = 1f;
    private float attackCooldown;
    public float damage = 1f;
    public float speed = 1f;
    public int attackEffectIdx = 1;

    public void EnemyInit()
    {
        gameObject.SetActive(false);
    }
    public void EnemyReset()
    {
        transform.position = Vector3.zero + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f)); // 강제 위치 초기화
        currentHp = maxHp;
        target = null;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Dead) return;

        if (attackCooldown > 0f)
        {
            attackCooldown -= 1f * Time.deltaTime;
        }

        if (target == null || !target.gameObject.activeSelf)
        {
            target = null;
            var towers = BuildingManager.Instance.towers;
            float shortestDist = float.MaxValue;
            Unit tempTarget = null;
            foreach (var tower in towers)
            {
                float differ = Mathf.Abs(tower.transform.position.x - transform.position.x) + Mathf.Abs(tower.transform.position.y - transform.position.y);
                if (differ < shortestDist)
                {
                    shortestDist = differ;
                    tempTarget = tower;
                }
                if (shortestDist < 1f)
                    break;
            }
            if (tempTarget != null)
                target = tempTarget;
        }
        if (target == null)
        {
            rg.velocity = Vector2.zero;
            return;
        }

        Vector3 dir = target.transform.position - transform.position;

        if (dir.magnitude < 0.25f)
        {
            if (attackCooldown <= 0f)
            {
                target.Damage(damage);
                EffectManager.Instance.CallEffect(attackEffectIdx, transform.position);
                attackCooldown = attackSpeed;
            }
            rg.velocity = Vector2.zero;
        }
        else
        {
            dir.Normalize();
            //transform.position += speed * Time.deltaTime * dir;
            rg.velocity = speed * dir;
        }
    }
}
