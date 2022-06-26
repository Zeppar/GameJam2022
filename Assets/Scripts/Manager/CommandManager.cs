using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : IManager
{
    public List<string> commandList = new List<string> { "help", "ls", "cd", "kill", "babaisme", "clear" };
    public List<string> commandHistroy = new List<string>();
    public int commandHistoryPoint = 0;

    public bool IsCommandAvailable(string command)
    {
        string trimCommand = command.Trim();
        if(trimCommand.StartsWith("cd "))
        {
            string path = command.Replace("cd ", "");
            // 判断当前路径是否存在
            FileDetailInfo info = GameController.manager.GetManager<FileManager>().GetChild(GameController.manager.GetManager<FileManager>().curFileInfo.childDetailInfoList, path);
            if ((info != null && info.type == FileDetailType.DIR) || path == "..")
            {
                return true;
            }
            else
            {
                return false;
            }
        } else
        {
            if (commandList.Contains(trimCommand))
            {
                return true;
            }
        }
        return false;
    }

    public void UseCommond(string command)
    {
        commandHistroy.Add(command);
        commandHistoryPoint = commandHistroy.Count;

        string trimCommand = command.Trim();
        if (!IsCommandAvailable(trimCommand))
        {
            return;
        }

        if (trimCommand.StartsWith("cd "))
        {
            string path = command.Replace("cd ", "");
            if (path != "..")
            {
                // 判断当前路径是否存在
                FileDetailInfo info = GameController.manager.GetManager<FileManager>().GetChild(GameController.manager.GetManager<FileManager>().curFileInfo.childDetailInfoList, path);
                if (info != null && info.type == FileDetailType.DIR)
                {
                    GameController.manager.GetManager<FileManager>().curFileInfo = info;
                }
                else
                {
                    GameUI.instance.commandUI.AddErrorCommand("无效路径");
                }
            } else
            {
                // 进入上层文件夹
                if (GameController.manager.GetManager<FileManager>().curFileInfo != null 
                    && GameController.manager.GetManager<FileManager>().curFileInfo.parentInfo != null)
                {
                    GameController.manager.GetManager<FileManager>().curFileInfo = GameController.manager.GetManager<FileManager>().curFileInfo.parentInfo;
                    GameUI.instance.commandUI.AddTipCommand("进入上层文件夹");
                } else
                {
                    GameUI.instance.commandUI.AddErrorCommand("当前文件夹无上层文件夹");
                }
            }
        } else
        {
            switch (trimCommand)
            {
                case "help":
                    // 提示可以使用kill和ls命令
                    GameUI.instance.commandUI.AddTipCommand("kill:停止进程  ls:查看目录下所有文件");
                    break;
                case "ls":
                    int count = 0;
                    for (int i = 0; i < GameController.manager.GetManager<FileManager>().curFileInfo.childDetailInfoList.Count; i++)
                    {
                        if (GameController.manager.GetManager<FileManager>().curFileInfo.childDetailInfoList[i].showSelf || GameController.manager.userInfo.isTopUser)
                        {
                            count += 1;
                        }
                    }
                    if (count == 0)
                    {
                        GameUI.instance.commandUI.AddTipCommand("当前文件夹无文件");
                    } else
                    {
                        // 显示所有文件
                        for (int i = 0; i < GameController.manager.GetManager<FileManager>().curFileInfo.childDetailInfoList.Count; i++)
                        {
                            if (GameController.manager.GetManager<FileManager>().curFileInfo.childDetailInfoList[i].showSelf || GameController.manager.userInfo.isTopUser)
                            {
                                GameUI.instance.commandUI.AddFileCommand(GameController.manager.GetManager<FileManager>().curFileInfo.childDetailInfoList[i]);
                            }
                        }
                    }
                    break;
                case "kill":
                    // 根据权限判定是否可以成功
                    if(GameController.manager.userInfo.isTopUser)
                    {
                        GameUI.instance.commandUI.AddTipCommand("倒计时程序自动销毁！");
                        GameController.manager.MeetEnding(EndingType.KILL);
                    } else
                    {
                        GameUI.instance.commandUI.AddErrorCommand("权限不足，无法杀死程序");
                    }
                    break;
                case "babaisme":
                    // 获得最高级权限
                    GameController.manager.userInfo.isTopUser = true;
                    GameUI.instance.commandUI.AddTipCommand("你已激活TrcikOS, 并获得管理员权限。");
                    GameUI.instance.commandUI.AddTipCommand("你的权限被调整：查看隐藏目录由 不允许 调整为 允许");
                    GameUI.instance.commandUI.AddTipCommand("你的权限被调整：运行管理员指令由 不允许 调整为 允许");
                    GameUI.instance.activeOSText.gameObject.SetActive(false);
                    GameController.manager.tipsUI.Show("你的TrickOS已经激活");

                    break;
                case "clear":
                    GameUI.instance.commandUI.Clear();
                    break;
                default:
                    break;
            }
        }
    }

    public string GetHistory(int moveCount)
    {
        if (commandHistoryPoint + moveCount >= commandHistroy.Count || commandHistoryPoint + moveCount < 0)
        {
            Debug.Log("Can not get history");
            return null;
        }
        commandHistoryPoint += moveCount;
        return commandHistroy[commandHistoryPoint];

    }

    public string AutoCompletion(string command)
    {
        if (command.StartsWith("cd "))
        {
            string path = command.Replace("cd ", "");
            var childDetailInfoList = GameController.manager.GetManager<FileManager>().curFileInfo.childDetailInfoList;
            for (int i = 0; i < childDetailInfoList.Count; i++)
            {
                if (childDetailInfoList[i].showSelf && childDetailInfoList[i].name.StartsWith(path))
                {
                    return "cd " + childDetailInfoList[i].name;
                }
            }
        }
        else
        {
            for (int i =0; i < commandList.Count; i++)
            {
                if (commandList[i].StartsWith(command))
                {
                    return commandList[i];
                }
            }
        }
        return command;
    }
}
