using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bat_Movement_State : iState
{
    public Animator animator;
    public NavMeshAgent meshAgent;
    public Transform Player;
    public Enemy_Stats Enemy;
    private Bat_Machine_State _StateMachine;
    float timer = 0f;
    public void Enter()
    {
        animator = _StateMachine.animator;
        meshAgent = _StateMachine.meshAgent;
        Player = _StateMachine.Player;
        Enemy = _StateMachine.Enemy;
        animator.Play(_StateMachine.Fly);
        meshAgent.isStopped = true;
    }

    public void Exit()
    {
        meshAgent.isStopped = false;
    }

    public void Tick()
    {
        throw new System.NotImplementedException();
    }
}
