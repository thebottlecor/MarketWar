using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{

    public GameObject attackEffectSource;
    public int maxEffect = 10;
    public List<Effect> attackEffectPool;


    private void Start()
    {
        attackEffectPool = new List<Effect>(maxEffect);
        for (int i = 0; i < maxEffect; i++)
        {
            var obj = Instantiate(attackEffectSource, Vector3.zero, Quaternion.identity, transform);
            Effect effect = obj.GetComponent<Effect>();
            attackEffectPool.Add(effect);
        }
    }

    public void CallEffect(int idx, Vector3 pos)
    {

        switch (idx)
        {
            case 1:
                for (int i = 0; i < attackEffectPool.Count; i++)
                {
                    if (!attackEffectPool[i].gameObject.activeSelf)
                    {
                        attackEffectPool[i].EffectInit(pos);
                        break;
                    }
                }
                break;
        }
    }
}
