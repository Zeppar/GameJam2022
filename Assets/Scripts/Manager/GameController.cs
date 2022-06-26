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
    public TipsUI tipsUI;
    public EndUI endUI;

    public AudioSource audioSource;

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
            tipsUI.Show("恭喜你达成了新成就 （" + manager.userInfo.endingIdList.Count + "/7）");

        }
        if (manager.userInfo.endingIdList.Count >= 6)
        {
            GameController.manager.endUI.Show(
                "恭喜你，你成功了，集齐的所有成就\n" +
                "不用惊慌，确实你还差一个成就没有达成\n" +
                "但是我宣布你成功了\n" +
                "不用尝试再找了，这里其实一共只有六个成就\n\n" +
                "虽然你成功了，但你也失败了\n" +
                "你被我们制作组坑害了\n" +
                "有这时间，你原本可以好好学习、努力工作、陪陪家人\n" +
                "但你玩了游戏\n" +
                "这是一个陷阱，你失败了\n"
            );
        }
        GameUI.instance.HideGame();
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
