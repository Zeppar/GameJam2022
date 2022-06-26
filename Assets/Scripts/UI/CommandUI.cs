using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandUI : MonoBehaviour
{
    public CommandItem itemPrefab;
    public Transform content;
    public InputField commandInput;
    public ScrollRect scrollRect;
    public Text pathText;
    public List<CommandItem> commandItems = new List<CommandItem>();

    public void AddCommand(string text)
    {
        CommandItem item = Instantiate(itemPrefab);
        item.transform.SetParent(content, false);
        item.SetContent(text);
        StartCoroutine(ScrollToBottom());
        commandItems.Add(item);
    }

    public void AddErrorCommand(string text)
    {
        CommandItem item = Instantiate(itemPrefab);
        item.transform.SetParent(content, false);
        item.SetErrorContent(text);
        StartCoroutine(ScrollToBottom());
        commandItems.Add(item);
    }

    public void AddTipCommand(string text)
    {
        CommandItem item = Instantiate(itemPrefab);
        item.transform.SetParent(content, false);
        item.SetTipContent(text);
        StartCoroutine(ScrollToBottom());
        commandItems.Add(item);

    }

    public void AddFileCommand(FileDetailInfo info)
    {
        CommandItem item = Instantiate(itemPrefab);
        item.transform.SetParent(content, false);
        item.SetFileContent(info);

        StartCoroutine(ScrollToBottom());
        commandItems.Add(item);
    }

    public void ShowStartInfo()
    {
        AddTipCommand("欢迎使用JamShell 1.0.0 LTS (TrickOS 3.2.2 x86)");
        AddTipCommand("");
        AddTipCommand("系统状态： 已加载");
        AddTipCommand("激活状态： 未激活（您的计算机可能运行的是TrickOS的盗版副本）");
        AddTipCommand("当前版本： 3.2.2");
        AddTipCommand("你的TrickOS已经成功启动");
        AddTipCommand("");
        AddTipCommand("输入help已获取更多帮助");
    }


    public void Clear()
    {
        for (int i = 0; i < commandItems.Count; i++)
        {
            Destroy(commandItems[i].gameObject);
        }
        commandItems = new List<CommandItem>();
    }

    public void Restart()
    {
        Clear();
        ShowStartInfo();
        commandInput.text = "";
        gameObject.SetActive(false);
    }

    private IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0;
    }

    private void Start()
    {
        commandInput.ActivateInputField();
        ShowStartInfo();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (commandInput.text.Trim().Length != 0) {
                var text = commandInput.text.Trim();
                AddCommand(text);
                GameController.manager.GetManager<CommandManager>().UseCommond(text);
                commandInput.text = "";
            }
            commandInput.ActivateInputField();
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            var text = GameController.manager.GetManager<CommandManager>().GetHistory(-1);
            if (text != null)
            {
                commandInput.text = text;
                commandInput.MoveTextEnd(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var text = GameController.manager.GetManager<CommandManager>().GetHistory(1);
            if (text != null)
            {
                commandInput.text = text;
                commandInput.MoveTextEnd(false);

            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            var command = commandInput.text.Trim();
            commandInput.text = GameController.manager.GetManager<CommandManager>().AutoCompletion(command);
            commandInput.MoveTextEnd(false);
        }
    }
}
