using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private YouDiedScaler scaler;
    [SerializeField] private GameObject defPanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private float textSpawnDelay;

    [SerializeField] private List<Image> crosshairs;

    [SerializeField] private GameObject restartButton;
    [SerializeField] private TextMeshProUGUI youDiedText;

    public static Action OnPlayerDie;

    //private bool canChange = true;

    //public bool CanChange { get => canChange; set => canChange = value; }

    private void Awake()
    {
        OnPlayerDie += Die;
    }

    private void Die()
    {
        defPanel.SetActive(false);
        deadPanel.SetActive(true);
        StartCoroutine(WaitForSpawnText());
        StartCoroutine(WaitForSpawnButton());
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeCrosshairs(int crossToHide, int crossToShow)
    {
        //if(!canChange)
        //{
        //    return;
        //}
        crosshairs[crossToHide].DOFade(0f, 0.2f);
        crosshairs[crossToShow].DOFade(1f, 0.2f);
        if (crossToShow == 2)
        {
            crosshairs[crossToShow].transform.DOScale(new Vector3(2f, 2f, 2f), 0.2f);
        }
        if (crossToHide == 2)
        {
            crosshairs[crossToHide].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
        }
    }
    private IEnumerator WaitForSpawnText()
    {
        yield return new WaitForSecondsRealtime(textSpawnDelay);
        youDiedText.gameObject.SetActive(true);
        scaler.ToScale(youDiedText);
    }
    private IEnumerator WaitForSpawnButton()
    {
        yield return new WaitForSecondsRealtime(textSpawnDelay += 3.1f);
        restartButton.SetActive(true);

    }
    private void OnDestroy()
    {
        OnPlayerDie -= Die;
    }
}
