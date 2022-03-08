//Written by Marc Hagoriles

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapCameraSystem : MonoBehaviour
{
    [SerializeField] public GameObject SinglePlayerMinimap;
    [SerializeField] public GameObject MultiplayerMinimap;

    public static MinimapCameraSystem Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void OpenSinglePlayerMinimap()
    {
        SinglePlayerMinimap.SetActive(true);
    }
    
    public void OpenMultiplayerMinimap()
    {
        MultiplayerMinimap.SetActive(true);
    }

    public void CloseMinimap()
    {
        SinglePlayerMinimap.SetActive(false);
        MultiplayerMinimap.SetActive(false);
    }
}
