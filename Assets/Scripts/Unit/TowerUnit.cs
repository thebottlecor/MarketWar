using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : Unit
{

    public TowerSensor sensor;

    public int income = 0;

    public int upgrade = 0;
    public int maxUpgrade = 0;

    [HideInInspector] public int cost;

    public void SetUpgrade()
    {
        if (upgrade >= maxUpgrade) return;

        int newCost = (int)(BuildingManager.Instance.towerCost * Mathf.Pow(2, upgrade + 1));
        int upgradeCost = newCost - cost;

        if (BuildingManager.Instance.gold >= upgradeCost)
        {
            BuildingManager.Instance.AddGold(-1 * upgradeCost);
            upgrade++;
            if (sensor != null)
                sensor.UpgradeAdjust(upgrade);
            UpgradeAdjust();
            cost = newCost;
        }
    }

    private void UpgradeAdjust()
    {
        damage = baseDamage * Mathf.Pow(2f, upgrade);
        maxHp = baseMaxHp * Mathf.Pow(1.5f, upgrade);
        attackSpeed = Mathf.Max(0.1f, baseAttackSpeed + (-0.1f * upgrade));

        sprite.color = new Color(1f, 1f + (-0.25f * upgrade), 1f + (-0.25f * upgrade));

        // Hp Ç®È¸º¹
        SetHp(maxHp);     
    }
}
