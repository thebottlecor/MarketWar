using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Lobby : Singleton<Lobby>
{

    public GameObject lobbyUI;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);

        DOTween.Init();

    }

    protected override void AddListeners()
    {
        LoadingSceneManager.SceneLoadCompletedEvent += OnLoadingComplete;
    }
    protected override void RemoveListeners()
    {
        LoadingSceneManager.SceneLoadCompletedEvent -= OnLoadingComplete;
    }

    private void OnLoadingComplete(object sender, string e)
    {
        if (e.Equals("lobby"))
        {
            lobbyUI.SetActive(true);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void GameStart()
    {
        lobbyUI.SetActive(false);
        LoadingSceneManager.Instance.LobbyStart();
    }
}
