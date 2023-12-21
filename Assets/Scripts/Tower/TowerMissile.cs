using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMissile : MonoBehaviour
{

    public bool isUsing;
    private Unit source;
    public Unit target;

    public float damage = 5f;
    public float speed = 3f;

    public void Init(Unit source)
    {
        this.source = source;
        ResetObj();
    }
    private void ResetObj()
    {
        transform.position = source.transform.position;
        isUsing = false;
        target = null;
        gameObject.SetActive(false);
    }

    public void Use(Unit target)
    {
        this.target = target;
        isUsing = true;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 dir = target.transform.position - transform.position;

        if (dir.magnitude < 0.25f)
        {
            target.Damage(damage);
            ResetObj();
        }
        else
        {
            dir.Normalize();
            transform.position += speed * Time.deltaTime * dir;
        }
    }

}
