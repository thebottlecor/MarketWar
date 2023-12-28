using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{

    public GameObject canvas;
    public Image inner;

    public void Hide()
    {
        canvas.SetActive(false);
    }

    public void Show()
    {
        canvas.SetActive(true);
    }

    public void SetGauge(float value, float max)
    {
        float percent = value / max;
        inner.fillAmount = percent;
    }

}
