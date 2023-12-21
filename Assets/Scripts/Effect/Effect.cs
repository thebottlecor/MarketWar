using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void EffectInit(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }
}
