using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UserInfo
{
    public bool isTopUser;
    public List<EndingType> endingIdList = new List<EndingType>();
}

public enum EndingType
{
    PERSON1 = 0,
    PERSON2,
    AIRPLANE,
    KILL,
    URL,
    TRASH
}


public delegate void NoneParamCallback();
public class GameController : MonoBehaviour
{
    public static GameController manager = null;

    public UserInfo userInfo = new UserInfo();
    public float maxRemainTime;
    public float remainingTime;

    public ErrorUI errorUI;
    public ShowMsgUI showMsgUI;
    public FailUI failUI;



    private Dictionary<string, IManager> managerDict = new Dictionary<string, IManager>();



    private void Awake()
    {
        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        } else if(manager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RegisterManager();
        Init();
    }

    private void RegisterManager()
    {
        managerDict.Add(typeof(FileManager).ToString(), new FileManager());
        managerDict.Add(typeof(CommandManager).ToString(), new CommandManager());
    }

    private void Init()
    {
        GetManager<FileManager>().Init();
    }

    public T GetManager<T> ()
    {
        if(managerDict.ContainsKey(typeof(T).ToString()))
        {
            return (T)managerDict[typeof(T).ToString()];
        }
        return default(T);
    }

    public void MeetEnding(EndingType type)
    {
        if(!userInfo.endingIdList.Contains(type))
        {
            userInfo.endingIdList.Add(type);
            remainingTime = maxRemainTime;
        }
        GameUI.instance.HideGame();
    }
}
