using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    private float lastStateChangeTime = 0f;
    private float minStateInterval = 0.05f;
    public EnemyState CurrentState { get; private set; }

    public void Initialize(EnemyState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        if (Time.time - lastStateChangeTime < minStateInterval)
            return;

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
        lastStateChangeTime = Time.time;
    }
}
