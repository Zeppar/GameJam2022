using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailUI : MonoBehaviour
{
    public Button retryBtn;
    public Text showText;

    private void Start()
    {
        retryBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            GameController.manager.remainingTime = GameController.manager.maxRemainTime;
            GameUI.instance.countDown.gameObject.SetActive(true);
        });
    }

    public void Show()
    {
        gameObject.SetActive(true);
        showText.text = "结局完成 : " + GameController.manager.userInfo.endingIdList.Count + "/7";
    }
}
