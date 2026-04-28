using System.Collections;
using UnityEngine;

public class PassiveAI : AIUpdate
{
    public override void AIDoSomething()
    {
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("I am passive");
        AIDoSomething();
    }
}
