using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time = 0;
    private bool toCount = false;

    private void Update()
    {
        if (toCount)
        {
            time += Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(time);
        toCount = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        toCount = true;
        time = 0;
    }
}
