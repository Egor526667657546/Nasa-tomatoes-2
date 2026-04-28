using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    protected string[] states = new string[] {"Idle", "Patrol", "Attack" };
    public abstract void Enter();
    public abstract void MyUpdate();
    public abstract void Exit();
}