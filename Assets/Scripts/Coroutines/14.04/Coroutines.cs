using System.Collections;
using UnityEngine;

public class Coroutines : MonoBehaviour
{
    [SerializeField] private GameObject gm;
    [SerializeField] private float time;

    private void Start()
    {
        StartCoroutine(MakeBlack(gm, time));
    }
    private IEnumerator MakeBlack(GameObject gm, float time)
    {
        Color startColor = gm.GetComponent<MeshRenderer>().material.color;
        float currentTime = 0f;
        while (currentTime < time)
        {
            gm.GetComponent<MeshRenderer>().material.color = Color.Lerp(startColor, new Color(0f, 0f, 0f), currentTime / time);
            yield return null;
            currentTime += Time.deltaTime;
        }
        gm.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f);
    }
}
