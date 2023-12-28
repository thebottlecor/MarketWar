using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{

    // 이펙트 라이브러리
    public GameObject attackEffectSource;
    public List<Effect> attackEffectPool;

    public GameObject repairEffectSource;
    public List<Effect> repairEffectPool;

    private void Start()
    {
        CreateEffectPool(ref attackEffectPool, 10, attackEffectSource);
        CreateEffectPool(ref repairEffectPool, 2, repairEffectSource);
    }

    private void CreateEffectPool(ref List<Effect> pool, int max, GameObject source)
    {
        pool = new List<Effect>(max);
        for (int i = 0; i < max; i++)
        {
            var obj = Instantiate(source, Vector3.zero, Quaternion.identity, transform);
            Effect effect = obj.GetComponent<Effect>();
            pool.Add(effect);
        }
    }

    public void CallEffect(int idx, Vector3 pos)
    {

        switch (idx)
        {
            case 1:
                SetEffect(pos, attackEffectPool);
                break;
            case 2:
                SetEffect(pos, repairEffectPool);
                break;
        }
    }

    private void SetEffect(Vector3 pos, List<Effect> pool)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                pool[i].EffectInit(pos);
                break;
            }
        }
    }
}
