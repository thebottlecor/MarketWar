using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{

    public Unit target;

    public Rigidbody2D rg;

    public int attackEffectIdx = 1;

    public void EnemyInit()
    {
        gameObject.SetActive(false);
    }
    public void EnemyReset()
    {
        Vector3 randomPos = Vector3.zero;
        int randomX = Random.Range(0, 3);
        if (randomX == 0)
        {
            randomPos.x = UnityEngine.Random.Range(EnemyManager.Instance.playArea.x, EnemyManager.Instance.playArea.x + 1f);
            randomPos.y = UnityEngine.Random.Range(-1f * EnemyManager.Instance.playArea.y, EnemyManager.Instance.playArea.y);
        }
        else if (randomX == 1)
        {
            randomPos.x = -1f * UnityEngine.Random.Range(EnemyManager.Instance.playArea.x, EnemyManager.Instance.playArea.x + 1f);
            randomPos.y = UnityEngine.Random.Range(-1f * EnemyManager.Instance.playArea.y, EnemyManager.Instance.playArea.y);
        }
        else
        {
            int randomY = Random.Range(0, 2);
            if (randomY == 0)
            {
                randomPos.x = UnityEngine.Random.Range(-1f * EnemyManager.Instance.playArea.x, EnemyManager.Instance.playArea.x);
                randomPos.y = UnityEngine.Random.Range(EnemyManager.Instance.playArea.y, EnemyManager.Instance.playArea.y + 1f);
            }
            else
            {
                randomPos.x = UnityEngine.Random.Range(-1f * EnemyManager.Instance.playArea.x, EnemyManager.Instance.playArea.x);
                randomPos.y = -1f * UnityEngine.Random.Range(EnemyManager.Instance.playArea.y, EnemyManager.Instance.playArea.y + 1f);
            }
        }

        transform.position = EnemyManager.Instance.startPos + randomPos; // 강제 위치 초기화
        StatAdjust();
        currentHp = maxHp;
        predictDamage = 0;
        target = null;
        gameObject.SetActive(true);
    }

    private void StatAdjust()
    {
        int wave = Mathf.Max(0, WaveManager.Instance.wave - 1);

        damage = baseDamage * Mathf.Pow(1.2f, wave);
        maxHp = baseMaxHp * Mathf.Pow(1.25f, wave);
        speed = Mathf.Min(6f, baseSpeed + (wave / 5) * 0.3f);
    }

    private void Update()
    {
        if (Dead) return;

        if (currentAttackCooldown > 0f)
        {
            currentAttackCooldown -= 1f * Time.deltaTime;
        }

        Unit player = PlayerController.Instance.player;
        target = player;
        //Vector3 playerPos = player.transform.position;
        //float playerDist = Mathf.Abs(playerPos.x - transform.position.x) + Mathf.Abs(playerPos.y - transform.position.y);
        //if (playerDist < 1f)
        //{
        //    target = player;
        //}
        //else if (target == player)
        //{
        //    target = null;
        //}

        if (target == null || !target.gameObject.activeSelf)
        {
            target = null;
            //var towers = BuildingManager.Instance.towers;
            //float shortestDist = float.MaxValue;
            //Unit tempTarget = null;
            //foreach (var tower in towers)
            //{
            //    float differ = Mathf.Abs(tower.transform.position.x - transform.position.x) + Mathf.Abs(tower.transform.position.y - transform.position.y);
            //    if (differ < shortestDist)
            //    {
            //        shortestDist = differ;
            //        tempTarget = tower;
            //    }
            //    if (shortestDist < 1f)
            //        break;
            //}
            //if (tempTarget != null)
            //    target = tempTarget;
        }
        if (target == null)
        {
            rg.velocity = Vector2.zero;
            return;
        }

        Vector3 dir = target.transform.position - transform.position;

        if (dir.magnitude < 0.5f)
        {
            if (currentAttackCooldown <= 0f)
            {
                target.Damage(damage);
                EffectManager.Instance.CallEffect(attackEffectIdx, transform.position);
                currentAttackCooldown = attackSpeed;
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

    public void CheckChangeTarget(Unit attacker)
    {
        if (target != null)
        {
            float prevDist = Mathf.Abs(target.transform.position.x - transform.position.x) + Mathf.Abs(target.transform.position.y - transform.position.y);
            float newDist = Mathf.Abs(attacker.transform.position.x - transform.position.x) + Mathf.Abs(attacker.transform.position.y - transform.position.y);

            if (prevDist > newDist)
            {
                target = attacker;
            }
        }
        else
        {
            target = attacker;
        }
    }
}
