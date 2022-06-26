using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingItem : MonoBehaviour
{
    public Text showText;
    public EndingType type;

    private void Update()
    {
        showText.color = GameController.manager.userInfo.endingIdList.Contains(type) ? Color.green : Color.gray;
    }
}
