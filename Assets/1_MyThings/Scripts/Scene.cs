using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public GameObject p;
    bool a = false;
    public GameObject s;
    bool b = false;


    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    void Start()
    {
        s.SetActive(b);
        p.SetActive(a);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            a = !a;
            p.SetActive(a);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu_Game");
    }
    public void OpenSet()
    {
        b = true;
        a = false;
        s.SetActive(b);
    }

    public void CloseSet()
    {
        b = false;
        a = true;
        s.SetActive(b);
    }
}