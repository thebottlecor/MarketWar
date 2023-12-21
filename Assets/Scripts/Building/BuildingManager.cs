using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{

    public HashSet<TowerUnit> towers;

    public GameObject towerSource;

    public int gold;
    public TextMeshProUGUI goldTMP;
    public Transform tempPlayerCursor;

    protected override void AddListeners()
    {
        Unit.dieEvent += OnUnitDie;
    }

    protected override void RemoveListeners()
    {
        Unit.dieEvent -= OnUnitDie;
    }

    private void OnUnitDie(object sender, bool e)
    {
        if (sender is TowerUnit)
        {
            TowerUnit tower = sender as TowerUnit;
            if (towers.Contains(tower))
                towers.Remove(tower);
        }
        else if (sender is EnemyUnit)
        {
            gold++;
            goldTMP.text = $"Gold : {gold}";
        }
    }

    protected override void Awake()
    {
        base.Awake();

        towers = new HashSet<TowerUnit>();

        gold = 30;
        goldTMP.text = $"Gold : {gold}";
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            if (gold >= 10)
            {
                var obj = Instantiate(towerSource, mousePos, Quaternion.identity);
                TowerUnit unit = obj.GetComponent<TowerUnit>();
                towers.Add(unit);
                gold -= 10;
                goldTMP.text = $"Gold : {gold}";
            }
        }

        tempPlayerCursor.position = mousePos;
    }
}
