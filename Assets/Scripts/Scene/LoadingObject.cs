using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingObject : EventListener
{


    public Slider loadingBar;
    public TextMeshProUGUI loadingTMP;

    protected override void AddListeners()
    {
        LoadingSceneManager.SceneLoadingEvent += OnSceneLoading;
        LoadingSceneManager.SceneLoadCompletedEvent += OnLoadingComplete;
    }

    protected override void RemoveListeners()
    {
        LoadingSceneManager.SceneLoadingEvent -= OnSceneLoading;
        LoadingSceneManager.SceneLoadCompletedEvent -= OnLoadingComplete;
    }

    private void Awake()
    {
        loadingTMP.text = string.Empty;
        loadingBar.value = 0;
    }

    private void OnSceneLoading(object sender, float e)
    {
        loadingTMP.text = $"Loading {e:P0}";
        loadingBar.value = e;
    }

    private void OnLoadingComplete(object sender, string e)
    {
        if (e.Equals("lobby"))
        {
            gameObject.SetActive(false);
        }
    }

}
