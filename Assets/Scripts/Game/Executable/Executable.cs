using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Executable : MonoBehaviour
{
    public bool isRunning = false;
    public virtual void Execute()
    {
        isRunning = true;
    }
    public virtual void Exit()
    {
        isRunning = false;
    }

    public virtual void ExecuteUpdate() {}
    public virtual void ExecuteFixedUpdate() {}

    private void Update()
    {
        if (isRunning == false) { return; }
        ExecuteUpdate();
    }

    private void FixedUpdate()
    {
        if (isRunning == false) { return; }
        ExecuteFixedUpdate();
    }
}
