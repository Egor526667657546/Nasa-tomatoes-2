using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("створено");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
