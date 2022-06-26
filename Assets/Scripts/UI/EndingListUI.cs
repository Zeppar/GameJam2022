using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingListUI : MonoBehaviour
{
    public Image sliderImage;
    private void Update()
    {
        sliderImage.fillAmount = GameController.manager.userInfo.endingIdList.Count / 7.0f;
    }
}
