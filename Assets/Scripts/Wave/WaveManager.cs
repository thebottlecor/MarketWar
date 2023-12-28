using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    public float playTime;

    public int waveGold = 30;

    public float firstWaveTime = 10f;
    public float waveInterval = 60f;
    private float nextTime;

    public int wave = 0;
    public TextMeshProUGUI waveTMP;

    private void Update()
    {
        playTime += 1f * Time.deltaTime;

        if (wave == 0)
        {
            nextTime = firstWaveTime - playTime;
            if (playTime >= firstWaveTime)
            {
                NewWave();
            }
        }
        else
        {
            nextTime = waveInterval - playTime;
            if (playTime >= waveInterval)
            {
                NewWave();
            }
        }

        waveTMP.text = $"WAVE {wave}\nNext {nextTime:F0}s";
    }

    private void NewWave()
    {
        if (wave > 0)
        {
            BuildingManager.Instance.AddGold(waveGold);
        }

        playTime = 0f;
        wave++;

        BuildingManager.Instance.CalcIncome();
        EnemyManager.Instance.NewWave();

        if (wave > 0 && wave % 10 == 0)
        {
            EnemyManager.Instance.BossWave();
        }
    }

}
