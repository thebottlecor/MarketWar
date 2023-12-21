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

    public float cooldown = 1f;
    private float cooldown_current;

    private void Awake()
    {
        enemies = new HashSet<Unit>();

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
        if (cooldown_current <= 0f)
        {
            foreach (var enemy in enemies)
            {
                if (!enemy.Dead)
                {
                    for (int i = 0; i < missilePool.Count; i++)
                    {
                        if (!missilePool[i].isUsing)
                        {
                            missilePool[i].Use(enemy);
                            cooldown_current = cooldown;
                            break;
                        }
                    }
                    break;
                }
            }
        }
        else
        {
            cooldown_current += -1f * Time.deltaTime;
        }
    }
}
