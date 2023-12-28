using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{


    public GameObject enemySource;
    public int maxEnemy = 100;
    public List<EnemyUnit> enemyPool;

    public GameObject bossSource;
    public int maxBoss = 1;
    public List<EnemyUnit> bossPool;

    private int currentWave;

    public float cooldown = 3f;
    private float cooldown_current;

    public Vector3 startPos = new Vector3(-1000, -1000, 0);
    public Vector2 playArea = new Vector2(10f, 10f);


    private void Start()
    {
        SetEnemyPool(ref enemyPool, maxEnemy, enemySource);
        SetEnemyPool(ref bossPool, maxBoss, bossSource);

        currentWave = 1;
    }

    private void SetEnemyPool(ref List<EnemyUnit> pool, int max, GameObject source)
    {
        pool = new List<EnemyUnit>(max);
        for (int i = 0; i < max; i++)
        {
            var obj = Instantiate(source, startPos, Quaternion.identity, transform);
            EnemyUnit unit = obj.GetComponent<EnemyUnit>();
            unit.EnemyInit();
            pool.Add(unit);
        }
    }


    //private void Update()
    //{
    //    if (cooldown_current <= 0f)
    //    {
    //        OldWave();
    //    }
    //    else
    //    {
    //        cooldown_current += -1f * Time.deltaTime;
    //    }
    //}

    //private void OldWave()
    //{
    //    int wave = currentWave;
    //    cooldown_current = cooldown;
    //    for (int i = 0; i < enemyPool.Count; i++)
    //    {
    //        if (!enemyPool[i].gameObject.activeSelf)
    //        {
    //            enemyPool[i].EnemyReset();
    //            wave--;
    //        }
    //        if (wave <= 0)
    //            break;
    //    }
    //    currentWave++;
    //}

    public void NewWave()
    {
        int wave = 25;
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (!enemyPool[i].gameObject.activeSelf)
            {
                enemyPool[i].EnemyReset();
                wave--;
            }
            if (wave <= 0)
                break;
        }
    }

    public void BossWave()
    {
        int wave = 1;
        for (int i = 0; i < bossPool.Count; i++)
        {
            if (!bossPool[i].gameObject.activeSelf)
            {
                bossPool[i].EnemyReset();
                wave--;
            }
            if (wave <= 0)
                break;
        }
    }

}
