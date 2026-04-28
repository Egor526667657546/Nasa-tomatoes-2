using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FireDelay : MonoBehaviour
{

    public async void WaitForSeconds(float time)
    {
        float timer = 0f;
        int normalTime = Convert.ToInt32(time);
        while (timer < time)
        {
            timer += Time.deltaTime;
            await Task.Delay(normalTime);
        }
        Debug.Log("Shot");
    }
}
