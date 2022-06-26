using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsUI : MonoBehaviour
{
    public Button closeBtn;
    public Text showText;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 5)
        {
            time = 0;
            gameObject.SetActive(false);
        }
    }

    public void Show(string text)
    {
        gameObject.SetActive(true);
        showText.text = text;

    }
}
