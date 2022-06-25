using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandItem : MonoBehaviour
{
    public Text showText;
    public Button selfBtn;
    private FileDetailInfo info;

    private void Start()
    {
        selfBtn.onClick.AddListener(() =>
        {
            if(info == null)
            {
                return;
            }
            if(info.type == FileDetailType.TXT)
            {
                GameUI.instance.showMsgUI.Show(info.showText);
            } else if(info.type == FileDetailType.EXE)
            {
                if (info.exeErrorText.Length == 0)
                {
                    GameUI.instance.ShowGame(info.exeGameId);
                } else
                {
                    GameController.manager.errorUI.Show(info.exeErrorText);
                }
            } else if(info.type == FileDetailType.HTML)
            {
                Application.OpenURL(info.webUrl);
            }
        });
    }

    public void SetContent(string text)
    {
        showText.text = text;
        if(GameController.manager.GetManager<CommandManager>().IsCommandAvailable(text))
        {
            showText.color = Color.green;
        } else
        {
            showText.color = Color.grey;
            // 提示无效命令
            GameUI.instance.commandUI.AddErrorCommand("无效命令...");
        }
    }

    public void SetErrorContent(string text)
    {
        showText.text = text;
        showText.color = Color.red;
    }

    public void SetTipContent(string text)
    {
        showText.text = text;
        showText.color = Color.yellow;
    }

    public void SetFileContent(FileDetailInfo info)
    {
        this.info = info;
        showText.text = info.name;
        showText.color = Color.yellow;
    }
}
