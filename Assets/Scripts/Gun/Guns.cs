using UnityEngine;

public class Guns : MonoBehaviour
{
    public GameObject gun1;
    public GameObject gun2;

    void Start()
    {

        gun1.SetActive(true);
        gun2.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun1.SetActive(true);
            gun2.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gun1.SetActive(false);
            gun2.SetActive(true);
        }
    }
}