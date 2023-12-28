using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMissile : MonoBehaviour
{

    public bool isUsing;
    private Unit source;
    private Unit target;
    private Vector3 nullPos;

    public float speed = 3f;

    public void Init(Unit source)
    {
        this.source = source;
        ResetObj();
        nullPos = Vector3.negativeInfinity;
    }
    private void ResetObj()
    {
        transform.position = source.transform.position;
        isUsing = false;
        if (target != null) target.predictDamage -= source.damage;
        target = null;
        gameObject.SetActive(false);
        nullPos = Vector3.negativeInfinity;
    }

    public void Use(Unit target)
    {
        transform.position = source.transform.position;
        this.target = target;
        this.target.predictDamage += source.damage;
        isUsing = true;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (target != null)
        {
            if (target.Dead)
            {
                nullPos = target.transform.position;
                if (target != null) target.predictDamage -= source.damage;
                target = null;
                return;
            }

            Vector3 dir = target.transform.position - transform.position;

            if (dir.magnitude < 0.25f)
            {
                target.Damage(source.damage);
                if (target is EnemyUnit)
                {
                    (target as EnemyUnit).CheckChangeTarget(source);
                }
                ResetObj();
            }
            else
            {
                dir.Normalize();
                transform.position += speed * Time.deltaTime * dir;
            }
        }
        else
        {
            NullPosMove();
        }
    }

    private void NullPosMove()
    {
        if (nullPos != Vector3.negativeInfinity)
        {
            Vector3 dir = nullPos - transform.position;

            if (dir.magnitude < 0.25f)
            {
                ResetObj();
            }
            else
            {
                dir.Normalize();
                transform.position += speed * Time.deltaTime * dir;
            }
        }
    }

}
