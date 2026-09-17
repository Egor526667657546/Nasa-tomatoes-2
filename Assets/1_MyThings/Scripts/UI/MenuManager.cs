using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject controlsPanel;

    private bool isPaused = false;


    public void OpenPause()
    {
        controlsPanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ClosePause()
    {
        controlsPanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
    public void TogglePanel()
    {
        mainPanel.SetActive(!mainPanel.activeSelf);
    }

    public void OpenPanel()
    {
        mainPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        mainPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void LoadLoading()
    {
        SceneManager.LoadScene("Loading");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}