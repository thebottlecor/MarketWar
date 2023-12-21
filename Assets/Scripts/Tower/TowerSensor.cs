using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSensor : MonoBehaviour
{

    public Unit owner;
    public TowerAttacker attacker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponentInParent<Unit>(true);

        if (owner.alliance != unit.alliance)
        {
            attacker.AddEnemy(unit);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Unit unit = collision.GetComponentInParent<Unit>(true);

        if (owner.alliance != unit.alliance)
        {
            attacker.RemoveEnemy(unit);
        }
    }
}
