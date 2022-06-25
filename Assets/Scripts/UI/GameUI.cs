﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI instance = null;
    public CommandUI commandUI;
    public ShowMsgUI showMsgUI;
    public Transform gameParent;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ShowGame(string name)
    {
        Debug.LogError("启动游戏 : " + name);
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefab/" + name));
        go.transform.SetParent(gameParent, false);
    }
    
}
