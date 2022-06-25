using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : Executable
{
    public enum PersonMode { mode1, mode2 }

    public float speed = 200;
    public float jumpFoucus = 500;
    public float gravity = 9.8f;
    public float obstacleDistance = 60;

    public Rigidbody2D rigidbody2d;
    public LayerMask groundLayer;
    public PersonMode personMode;
    
    public bool isOnGround = false;

    private void Start()
    {
        // 测试代码，正常情况下应该由Manager执行Execute
        Execute();
    }

    public override void Execute()
    {
        base.Execute();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void ExecuteUpdate()
    {
        base.ExecuteUpdate();
        Move();
        GroundCheck();
    }

    void GroundCheck()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, obstacleDistance, groundLayer);

        if (ray.collider != null)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, rigidbody2d.velocity.y - gravity);
        }
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime,
                transform.position.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime,
                transform.position.y);
        }
        if (Input.GetMouseButtonDown(0) && isOnGround)
        {
            rigidbody2d.velocity = new Vector2(0, jumpFoucus);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Countdown"))
        {
            if (personMode == PersonMode.mode1)
            {
                collision.gameObject.GetComponent<Countdown>().AddTime();
            }
            else
            {
                collision.gameObject.GetComponent<Countdown>().Rotate(transform.position);
            }
        }
    }

}
