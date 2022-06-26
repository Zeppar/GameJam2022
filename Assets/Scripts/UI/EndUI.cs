using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    public Button exitBtn;
    public Text showText;

    // Start is called before the first frame update
    void Start()
    {
        exitBtn.onClick.AddListener(() =>
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        });
    }

    public void Show(string text)
    {
        gameObject.SetActive(true);
        showText.text = text;

    }
}
