using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailUI : MonoBehaviour
{
    public Button retryBtn;
    public Button exitBtn;
    public Text showText;

    private void Start()
    {
        retryBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            GameUI.instance.HideGame();
            GameController.manager.remainingTime = GameController.manager.maxRemainTime;
            GameUI.instance.commandUI.Restart();
            GameUI.instance.countDown.gameObject.SetActive(true);
        });

        exitBtn.onClick.AddListener(() =>
        {
            GameController.manager.endUI.Show(
                "我们达到了我们的目的，你浪费了很多时间\n" +
                "你没有收集到所有的成就\n" +
                "你选择退出了游戏，但没有关系\n" +
                "下次如果有兴趣，欢迎再来试试\n" +
                "或者访问我们的网站获得一些小提示？\n"
            );
        });
    }

    public void Show()
    {
        gameObject.SetActive(true);
        showText.text = "成就完成 : " + GameController.manager.userInfo.endingIdList.Count + "/7";
    }
}
