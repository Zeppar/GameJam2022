using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorUI : MonoBehaviour
{
    public Text showText;
    public Button confirmBtn;

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
