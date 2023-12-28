using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{

    public HashSet<Unit> towers;
    public HashSet<TowerUnit> incomeTowers;

    public GameObject towerSource;
    public int towerCost = 100;
    public GameObject incomeTowerSource;
    public int incomeTowerCost = 50;

    public int initGold = 150;
    public int gold;
    public TextMeshProUGUI goldTMP;

    public Transform tempGridCursor;
    public Vector2 gridSize = new Vector2(1f, 1f);
    public Dictionary<Vector2, TowerUnit> gridBuilding = new Dictionary<Vector2, TowerUnit>();

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
            if (incomeTowers.Contains(tower))
                incomeTowers.Remove(tower);
            if (gridBuilding.ContainsKey(tower.transform.position))
            {
                gridBuilding.Remove(tower.transform.position);
                AddGold(tower.cost / 2);
            }
        }
        else if (sender is PlayerUnit)
        {
            PlayerUnit player = sender as PlayerUnit;
            if (towers.Contains(player))
                towers.Remove(player);
        }
        else if (sender is EnemyUnit)
        {
            AddGold(1);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        towers = new HashSet<Unit>();
        incomeTowers = new HashSet<TowerUnit>();

        gold = initGold;
        goldTMP.text = $"Gold : {gold}";
    }

    private void Start()
    {
        towers.Add(PlayerController.Instance.player);
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        int offset = -1;
        mousePos.x = Mathf.Round((mousePos.x - offset) / gridSize.x) * gridSize.x + offset;
        mousePos.y = Mathf.Round((mousePos.y - offset) / gridSize.y) * gridSize.y + offset;

        tempGridCursor.position = mousePos;

        bool exist = false;
        if (gridBuilding.ContainsKey(mousePos))
        {
            tempGridCursor.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
            exist = true;
        }
        else
        {
            tempGridCursor.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f);
        }

        float dist = Vector2.Distance(PlayerController.Instance.player.transform.position, mousePos);
        if (dist > 3f)
        {
            tempGridCursor.gameObject.SetActive(false);
            return;
        }
        else
        {
            tempGridCursor.gameObject.SetActive(true);
        }

        if (!Utils.MouseOverUI() )
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (exist)
                {
                    //var temp = Physics2D.OverlapCircle(mousePos, 0.1f, new ContactFilter2D { layerMask = LayerMask.NameToLayer("", useLayerMask = true });

                    gridBuilding[mousePos].SetUpgrade();

                }
                else
                {
                    if (gold >= towerCost)
                    {
                        var obj = Instantiate(towerSource, mousePos, Quaternion.identity);
                        TowerUnit unit = obj.GetComponent<TowerUnit>();
                        towers.Add(unit);
                        AddGold(-1 * towerCost);
                        unit.cost = towerCost;

                        gridBuilding.Add(mousePos, unit);
                    }
                }
            }
            if (exist)
                return;

            if (Input.GetMouseButtonDown(1))
            {
                if (gold >= incomeTowerCost)
                {
                    var obj = Instantiate(incomeTowerSource, mousePos, Quaternion.identity);
                    TowerUnit unit = obj.GetComponent<TowerUnit>();
                    towers.Add(unit);
                    incomeTowers.Add(unit);
                    AddGold(-1 * incomeTowerCost);
                    unit.cost = incomeTowerCost;

                    gridBuilding.Add(mousePos, unit);
                }
            }
        }
    }

    public void CalcIncome()
    {
        int totalIncome = 0;
        foreach (var tower in incomeTowers)
        {
            totalIncome += tower.income;
        }
        AddGold(totalIncome);
        Debug.Log(totalIncome);
    }

    public void AddGold(int value)
    {
        gold += value;
        goldTMP.text = $"Gold : {gold}";
    }
}
