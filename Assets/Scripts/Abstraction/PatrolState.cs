using UnityEngine;

public class PatrolState : AIState
{
    [SerializeField] private int state;
    private string currentState;
    private void Awake()
    {
        if (state > states.Length - 1)
        {
            Debug.Log("This state doesn`t exist");
            return;
        }
        currentState = states[state];
    }
    public override void Enter()
    {
        Debug.Log($"Do enter in {currentState} state");
    }

    public override void Exit()
    {
        Debug.Log($"Do exit in {currentState} state");
    }

    public override void MyUpdate()
    {
        Debug.Log($"Do update in {currentState} state");
    }
}
