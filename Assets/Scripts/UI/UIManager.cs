using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private YouDiedScaler scaler;
    [SerializeField] private GameObject defPanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private float textSpawnDelay;

    [SerializeField] private GameObject restartButton;
    [SerializeField] private TextMeshProUGUI youDiedText;

    public static Action OnPlayerDie;

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
