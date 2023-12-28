using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{


    private void Update()
    {
        AddHp(baseHpRegen * Time.deltaTime);
    }
}
