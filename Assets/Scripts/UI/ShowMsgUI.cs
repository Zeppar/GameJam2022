﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMsgUI : MonoBehaviour
{
    public Button confirmBtn;
    public Text showText;

    private void Start()
    {
        confirmBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void Show(string text)
    {
        gameObject.SetActive(true);
        showText.text = text;

    }
}
