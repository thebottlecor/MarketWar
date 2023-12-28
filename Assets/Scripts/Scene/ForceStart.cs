using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceStart : MonoBehaviour
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad()
    {
#if UNITY_EDITOR

        string sceneName = SceneManager.GetActiveScene().name;

        if (!sceneName.Equals("lobby"))
        {
            SceneManager.LoadScene("editor_lobby");
        }

#endif
    }


    private void Start()
    {
        Lobby.Instance.lobbyUI.SetActive(false);
        LoadingSceneManager.Instance.LobbyStart();
    }
}
