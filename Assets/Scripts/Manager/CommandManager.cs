using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : IManager
{
    public List<string> commandList = new List<string> { "help", "ls", "cd", "kill", "babaisme" };
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
                    GameUI.instance.commandUI.AddTipCommand("进入路径 : " + path);
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
                        if (GameController.manager.GetManager<FileManager>().curFileInfo.childDetailInfoList[i].showSelf)
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
                        GameUI.instance.commandUI.AddTipCommand("当前路径 : " + GameController.manager.GetManager<FileManager>().curFileInfo.path);
                        GameUI.instance.commandUI.AddTipCommand("文件列表如下");
                        for (int i = 0; i < GameController.manager.GetManager<FileManager>().curFileInfo.childDetailInfoList.Count; i++)
                        {
                            if (GameController.manager.GetManager<FileManager>().curFileInfo.childDetailInfoList[i].showSelf)
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
                        GameController.manager.userInfo.endingIdList.Add(EndingType.KILL);
                    } else
                    {
                        GameUI.instance.commandUI.AddErrorCommand("权限不足，无法杀死程序");
                    }
                    break;
                case "babaisme":
                    // 获得最高级权限
                    break;
                default:
                    break;
            }
        }
    }
}
