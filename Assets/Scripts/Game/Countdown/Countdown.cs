using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Countdown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Text text;
    public float addTimeMovement = 10;
    public bool canMove;

    private int addTimeCount;
    private bool isMoving;
    private Vector3 originPos;
    private bool enterCommand;


    private void Start()
    {
        GameController.manager.remainingTime = GameController.manager.maxRemainTime;
        originPos = transform.localPosition;
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
                // ��Ϸ����
                GameController.manager.MeetEnding(EndingType.PERSON2);

            }
        } else
        {
            if (GameController.manager.remainingTime >= 26)
            {
                // ��Ϸ����
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

        // move
        if (GameController.manager.userInfo.isTopUser && canMove && isMoving)
        {
            // 跟随鼠标移动
            transform.localPosition = GetScreenPosition();
        }
    }

    public Vector3 GetScreenPosition()
    {
        Vector2 uguiPos = Input.mousePosition;
        uguiPos.x -= Screen.width * 0.5f;
        uguiPos.y -= Screen.height * 0.5f;
        return uguiPos;
    }

    public void AddTime()
    {
        Debug.Log("ADD TIME");
        addTimeCount++;
        GameController.manager.remainingTime += 1.8f;
        if (addTimeCount % 2 == 0)
        {
            transform.position = new Vector3(transform.position.x + addTimeMovement, transform.position.y);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - addTimeMovement, transform.position.y);
        }
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if(GameController.manager.userInfo.isTopUser && canMove)
        {
            isMoving = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameController.manager.userInfo.isTopUser && canMove)
        {
            isMoving = false;
            transform.localPosition = originPos;
            if(enterCommand)
            {
                var fileInfo = GameController.manager.GetManager<FileManager>().curFileInfo;
                if(fileInfo != null && fileInfo.name == "trash")
                {
                    GameController.manager.MeetEnding(EndingType.TRASH);
                }
            }
        }
    }

     private void OnTriggerEnter2D(Collider2D other)
     {
        if(other.transform.gameObject.GetComponent<CommandUI>() != null)
        {
            enterCommand = true;
        }
     }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.gameObject.GetComponent<CommandUI>() != null)
        {
            enterCommand = false;
        }
    }
}
