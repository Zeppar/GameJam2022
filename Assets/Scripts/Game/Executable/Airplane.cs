using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : Executable
{
    private float xSpeed = 1;
    private float ySpeed = -0.5f;
    public float upSpeed = 0.2f;
    public float downSpeed = 0.05f;


    private void Start()
    {
        // 测试代码，正常情况下应该由Manager执行Execute
        Execute();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void ExecuteUpdate()
    {
        base.ExecuteUpdate();
        Move();
    }

    public void Move()
    {
        if (!isRunning) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            ySpeed += upSpeed;
        }

        transform.position = new Vector2(transform.position.x + xSpeed, transform.position.y + ySpeed);
        ySpeed -= downSpeed;

    }

    public override void Exit()
    {
        base.Exit();
        Destroy(gameObject);
    }

    public void WinGame()
    {
        Exit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 6 = Ground
        if (collision.gameObject.layer == 6)
        {
            Exit();
        }
        else if (collision.gameObject.name == "End")
        {
            WinGame();
            GameController.manager.MeetEnding(EndingType.AIRPLANE);
            GameController.manager.GetManager<FileManager>().SetFileState("passport.txt", true);
        }
    }
}
