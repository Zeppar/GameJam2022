using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public const int maxTime = 10;
    public Text text;

    public float remainingTime = maxTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime > 8 && remainingTime < 9
            && (Mathf.Approximately(transform.rotation.eulerAngles.z, 270)
            || Mathf.Approximately(transform.rotation.eulerAngles.z, 90)))
        {
            Debug.Log("888888");
        }
        if (remainingTime > 0)
        {
            text.text = ((int)remainingTime).ToString();

        }
        else
        {

        }
    }

    public void AddTime()
    {
        Debug.Log("ADD TIME");
        remainingTime += 1;
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
