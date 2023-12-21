using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{


    public GameObject enemySource;
    public int maxEnemy = 10;
    public List<EnemyUnit> enemyPool;

    private int currentWave;

    public float cooldown = 3f;
    private float cooldown_current;


    private void Start()
    {
        enemyPool = new List<EnemyUnit>(maxEnemy);
        for (int i = 0; i < maxEnemy; i++)
        {
            var obj = Instantiate(enemySource, Vector3.zero, Quaternion.identity, transform);
            EnemyUnit unit = obj.GetComponent<EnemyUnit>();
            unit.EnemyInit();
            enemyPool.Add(unit);
        }

        currentWave = 1;
    }


    private void Update()
    {
        if (cooldown_current <= 0f)
        {
            int wave = currentWave;
            cooldown_current = cooldown;
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
            currentWave++;
        }
        else
        {
            cooldown_current += -1f * Time.deltaTime;
        }
    }

}
