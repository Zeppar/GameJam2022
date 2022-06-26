using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CommandItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text showText;
    public Button selfBtn;
    private FileDetailInfo info;
    private Color originColor;

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
                GameController.manager.showMsgUI.Show(info.showText);
            } else if(info.type == FileDetailType.EXE)
            {
                bool findDll = false;
                if(info.parentInfo != null)
                {
                    string name = info.name.Split('.')[0];
                    for(int i = 0; i < info.parentInfo.childDetailInfoList.Count; i++)
                    {
                        if (info.parentInfo.childDetailInfoList[i].name == (name + ".dll") && info.parentInfo.childDetailInfoList[i].showSelf)
                        {
                            findDll = true;
                            break;
                        }
                    }
                }
                if (!info.needDll || findDll )
                {
                    GameUI.instance.ShowGame(info.exeGameId);
                } else
                {
                    GameController.manager.errorUI.Show(info.exeErrorText);
                }
            } else if(info.type == FileDetailType.HTML)
            {
                Application.OpenURL(info.webUrl);
                if (info.name.Equals("一个隐秘的角落.html"))
                {
                    GameController.manager.MeetEnding(EndingType.URL);
                }
            } else if(info.type == FileDetailType.DLL)
            {
                if(info.path.Contains("trash"))
                {
                    // 放回原文件夹
                    gameObject.SetActive(false);
                    GameController.manager.GetManager<FileManager>().SetFileState(info.name, true);
                    info.showSelf = false;
                } else
                {
                    // do nothing
                }
            }
        });
    }

    public void SetContent(string text)
    {
        showText.text = GameController.manager.GetManager<FileManager>().curFileInfo.path +"$ "+ text;
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
        showText.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(info != null && (info.type == FileDetailType.EXE || info.type == FileDetailType.HTML || info.type == FileDetailType.TXT))
        {
            originColor = showText.color;
            showText.color = Color.cyan;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (info != null && (info.type == FileDetailType.EXE || info.type == FileDetailType.HTML || info.type == FileDetailType.TXT))
        {
            showText.color = originColor;
        }
    }
}
