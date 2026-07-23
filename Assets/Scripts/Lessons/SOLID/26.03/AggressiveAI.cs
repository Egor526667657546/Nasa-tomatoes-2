using UnityEngine;

public class AggressiveAI : AIUpdate
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    private void Update()
    {
        AIDoSomething();
    }
    public override void AIDoSomething()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
