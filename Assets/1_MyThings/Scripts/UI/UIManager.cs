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
    [Header("General")]
    [SerializeField] private YouDiedScaler scaler;
    [SerializeField] private GameObject defPanel;
    [SerializeField] private float textSpawnDelay;

    [Header("Crosshairs")]
    [SerializeField] private List<Image> crosshairs;

    [Header("Ammo")]
    [SerializeField] private CanvasGroup ammoIm;
    [SerializeField] private CanvasGroup ammoCircle;
    [SerializeField] private CanvasGroup ammoCircleBack;
    [SerializeField] private TextMeshProUGUI ammoT;

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject exitButton;

    [Header("Dead screen")]
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject exitButtonDead;
    [SerializeField] private TextMeshProUGUI restartButtonText;
    [SerializeField] private TextMeshProUGUI exitButtonDeadText;
    [SerializeField] private TextMeshProUGUI youDiedText;

    public static Action OnPlayerDie;

    public GameObject DeadPanel { get => deadPanel;}

    //private bool canChange = true;

    //public bool CanChange { get => canChange; set => canChange = value; }

    private void Awake()
    {
        OnPlayerDie += Die;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !DeadPanel.activeSelf)
        {
            Pause();
        }
    }
    private void Die()
    {
        defPanel.SetActive(false);
        DeadPanel.SetActive(true);
        StartCoroutine(WaitForSpawnText());
        StartCoroutine(WaitForSpawnButtons());
    }
    private void Pause()
    {
        Time.timeScale = 1 - Time.timeScale;
        pausePanel.SetActive(!pausePanel.activeSelf);
        defPanel.SetActive(!defPanel.activeSelf);
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
        crosshairs[crossToShow].DOFade(0.863f, 0.2f);
        //if (crossToShow == 2)
        //{
        //    crosshairs[crossToShow].transform.DOScale(new Vector3(2f, 2f, 2f), 0.2f);
        //}
        //if (crossToHide == 2)
        //{
        //    crosshairs[crossToHide].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
        //}
    }
    public void ShowAmmo()
    {
        ammoT.DOFade(1f, 1f);
        ammoIm.DOFade(1f, 1f);
        ammoCircle.DOFade(1f, 1f);
        ammoCircleBack.DOFade(1f, 1f);
    }
    public void LeaveGame()
    {
        SceneManager.LoadScene("Menu");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    private IEnumerator WaitForSpawnText()
    {
        yield return new WaitForSecondsRealtime(textSpawnDelay);

        youDiedText.DOFade(1f, 0.2f).SetUpdate(true);
        scaler.ToScale(youDiedText);
    }

    private IEnumerator WaitForSpawnButtons()
    {
        yield return new WaitForSecondsRealtime(textSpawnDelay + 3.1f);

        restartButton.SetActive(true);
        restartButtonText.gameObject.SetActive(true);
        Debug.Log("aboba");
        restartButton.GetComponent<Image>().DOFade(1f, 0.5f).SetUpdate(true);
        restartButtonText.DOFade(1f, 0.5f).SetUpdate(true);


        exitButtonDead.SetActive(true);
        exitButtonDeadText.gameObject.SetActive(true);
        exitButtonDead.GetComponent<Image>().DOFade(1f, 0.5f).SetUpdate(true);
        exitButtonDeadText.DOFade(1f, 0.5f).SetUpdate(true);
    }
    private void OnDestroy()
    {
        OnPlayerDie -= Die;
    }
}
