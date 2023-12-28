using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttacker : MonoBehaviour
{

    public Unit owner;

    public HashSet<Unit> enemies;

    public TowerMissile missileSource;
    public int maxMissilePool = 1;
    public List<TowerMissile> missilePool;

    private void Start()
    {
        enemies = new HashSet<Unit>();

        // 주인이 없어진 미사일을 다른 주인에게 양도하는 작업 필요!
        missilePool = new List<TowerMissile>(maxMissilePool);
        for (int i = 0; i < maxMissilePool; i++)
        {
            var obj = Instantiate(missileSource, MissileManager.Instance.transform);
            obj.Init(owner);
            missilePool.Add(obj);
        }
    }

    public void AddEnemy(Unit unit)
    {
        if (!enemies.Contains(unit))
        {
            enemies.Add(unit);
        }
    }

    public void RemoveEnemy(Unit unit)
    {
        if (enemies.Contains(unit))
        {
            enemies.Remove(unit);
        }
    }

    private void Update()
    {
        if (owner is PlayerUnit)
        {
            if (PlayerController.Instance.IsCasting) return;
        }

        if (owner.currentAttackCooldown <= 0f)
        {
            float shorestDist = float.MaxValue;
            Unit target = null;
            foreach (var enemy in enemies)
            {
                if (!enemy.Dead && !enemy.PredictDead)
                {
                    float newDist = Mathf.Abs(enemy.transform.position.x - transform.position.x) + Mathf.Abs(enemy.transform.position.y - transform.position.y);
                    if (newDist < shorestDist)
                    {
                        shorestDist = newDist;
                        target = enemy;
                    }
                    if (shorestDist <= 0.5f)
                    {
                        break;
                    }
                }
            }

            if (target != null)
            {
                for (int i = 0; i < missilePool.Count; i++)
                {
                    if (!missilePool[i].isUsing)
                    {
                        missilePool[i].Use(target);
                        owner.currentAttackCooldown = owner.attackSpeed;
                        break;
                    }
                }
            }
        }
        else
        {
            owner.currentAttackCooldown += -1f * Time.deltaTime;
        }
    }
}
