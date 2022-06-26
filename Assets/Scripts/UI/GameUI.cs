using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance = null;
    public CommandUI commandUI;
    public Transform gameParent;
    public Countdown countDown;
    public Text activeOSText;
    public Image ESCTip;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ShowGame(string name)
    {
        Debug.LogError("启动游戏 : " + name);
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefab/" + name));
        go.transform.SetParent(gameParent, false);
        countDown.gameObject.SetActive(false);
    }
    
    public void HideGame()
    {
        for(int i = gameParent.childCount - 1; i >= 0; i--)
        {
            Destroy(gameParent.GetChild(i).gameObject);
        }
        countDown.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            commandUI.gameObject.SetActive(!commandUI.gameObject.activeSelf);
            ESCTip.gameObject.SetActive(!commandUI.gameObject.activeSelf);
        }
    }
}
