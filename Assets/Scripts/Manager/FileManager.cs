using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public enum FileDetailType
{
    DIR = 0,
    TXT = 1,
    EXE = 2,
    HTML = 3,
    DLL = 4
}

public class FileDetailInfo
{
    public FileDetailType type;
    public string name;
    public bool showSelf;
    public FileDetailInfo parentInfo;
    public int index = 0;
    // for dir
    public List<FileDetailInfo> childDetailInfoList = new List<FileDetailInfo>();
    // for txt
    public string showText;
    // for exe
    public string exeGameId;
    public string exeErrorText;
    // for html
    public string webUrl;
}

public class FileManager : IManager
{
    public FileDetailInfo curFileInfo = null;

    public FileDetailInfo GetChild(List<FileDetailInfo> list, string childName)
    {
        if (list.Count <= 0)
        {
            return null;
        }
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].name == childName)
            {
                return list[i];
            }
        }
        return null;
    }

    public void Init()
    {
        var text = Resources.Load<TextAsset>("fileData").text;
        JsonData data = JsonMapper.ToObject(text);
        curFileInfo = GetFileDetailInfo(data);
        curFileInfo.parentInfo = null;
    }

    private FileDetailInfo GetFileDetailInfo(JsonData detail)
    {
        FileDetailInfo info = new FileDetailInfo();
        info.type = (FileDetailType)(int)detail["type"];
        info.name = (string)detail["name"];
        if (info.type == FileDetailType.DIR)
        {
            JsonData child = detail["child"];
            for(int i = 0; i < child.Count; i++)
            {
                var childInfo = GetFileDetailInfo(child[i]);
                childInfo.parentInfo = info;
                info.childDetailInfoList.Add(childInfo); ;
            }
        }
        else if (info.type == FileDetailType.TXT)
        {
            info.showText = (string)detail["showText"];
        }
        else if (info.type == FileDetailType.EXE)
        {
            info.exeGameId = (string)detail["exeGameId"];
            info.exeErrorText = (string)detail["exeErrorText"];
        }
        else if (info.type == FileDetailType.HTML)
        {
            info.webUrl = (string)detail["webUrl"];
        }
        else if (info.type == FileDetailType.DLL)
        {
            // TODO
        }
        try
        {
            info.showSelf = (bool)detail["showSelf"];
        } catch
        {
            info.showSelf = true;
        }
        return info;
    }
}
