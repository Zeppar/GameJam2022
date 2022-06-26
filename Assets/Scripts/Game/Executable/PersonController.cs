using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    public GameObject mode1Go;
    public GameObject mode2Go;

    private void Start()
    {
        if(GameController.manager.userInfo.endingIdList.Contains(EndingType.PERSON1))
        {
            mode2Go.SetActive(true);
            mode1Go.SetActive(false);
        } else
        {
            mode2Go.SetActive(false);
            mode1Go.SetActive(true);
        }
    }
}
