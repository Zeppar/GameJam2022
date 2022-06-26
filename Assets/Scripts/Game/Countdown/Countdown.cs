using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text text;


    private void Start()
    {
        GameController.manager.remainingTime = GameController.manager.maxRemainTime;
    }

    // Update is called once per frame
    void Update()
    {
        GameController.manager.remainingTime -= Time.deltaTime;
        if (GameController.manager.userInfo.endingIdList.Contains(EndingType.PERSON1)) {
            if (GameController.manager.remainingTime > 8 && GameController.manager.remainingTime < 9
                && (Mathf.Approximately(Mathf.Abs(transform.rotation.eulerAngles.z), 270)
                || Mathf.Approximately(Mathf.Abs(transform.rotation.eulerAngles.z), 90)))
            {
                // 游戏结束
                GameController.manager.MeetEnding(EndingType.PERSON2);

            }
        } else
        {
            if (GameController.manager.remainingTime >= 26)
            {
                // 游戏结束
                GameController.manager.maxRemainTime = 30;
                GameController.manager.MeetEnding(EndingType.PERSON1);
            }
        }
        
        if (GameController.manager.remainingTime > 0)
        {
            text.text = ((int)GameController.manager.remainingTime).ToString();
        }
        else
        {
            text.text = "PC ERROR!";
            gameObject.SetActive(false);
            GameController.manager.failUI.Show();
        }
    }

    public void AddTime()
    {
        Debug.Log("ADD TIME");
        GameController.manager.remainingTime += 1.8f;
    }

    public void Rotate(Vector2 playerPosition)
    {
        if (playerPosition.x > transform.position.x)
        {
            transform.RotateAround(transform.position, Vector3.forward, 30);

        }
        else
        {
            transform.RotateAround(transform.position, Vector3.forward, -30);

        }
    }
}
