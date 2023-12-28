using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSensor : MonoBehaviour
{

    public Unit owner;
    public TowerAttacker attacker;

    public CircleCollider2D coll;
    public float baseRadius = 2.4f;

    public void UpgradeAdjust(int upgrade)
    {
        coll.radius = baseRadius + (1.5f * upgrade);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponentInParent<Unit>(true);

        if (unit != null && owner.alliance != unit.alliance)
        {
            attacker.AddEnemy(unit);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Unit unit = collision.GetComponentInParent<Unit>(true);

        if (unit != null && owner.alliance != unit.alliance)
        {
            attacker.RemoveEnemy(unit);
        }
    }
}
