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

    public void AddCommand(string text)
    {
        CommandItem item = Instantiate(itemPrefab);
        item.transform.SetParent(content, false);
        item.SetContent(text);
        StartCoroutine(ScrollToBottom());
    }

    public void AddErrorCommand(string text)
    {
        CommandItem item = Instantiate(itemPrefab);
        item.transform.SetParent(content, false);
        item.SetErrorContent(text);
        StartCoroutine(ScrollToBottom());
    }

    public void AddTipCommand(string text)
    {
        CommandItem item = Instantiate(itemPrefab);
        item.transform.SetParent(content, false);
        item.SetTipContent(text);
        StartCoroutine(ScrollToBottom());
    }

    public void AddFileCommand(FileDetailInfo info)
    {
        CommandItem item = Instantiate(itemPrefab);
        item.transform.SetParent(content, false);
        item.SetFileContent(info);

        StartCoroutine(ScrollToBottom());
    }

    private IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0;
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
    }
}
