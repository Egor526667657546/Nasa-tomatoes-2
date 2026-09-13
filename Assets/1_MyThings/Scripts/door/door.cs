using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public GameObject openText;

    private bool playerNear = false;
    private bool levelCompleted = false;

    private void Start()
    {
        openText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;

            if (!levelCompleted)
            {
                openText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            openText.SetActive(false);

            if (!levelCompleted)
            {
                animator.SetBool("Open", false);
            }
        }
    }

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E) && !levelCompleted)
        {
            animator.SetBool("Open", true);
            openText.SetActive(false);
        }
    }

    public void LevelCompleted()
    {
        levelCompleted = true;

        animator.SetBool("Open", true);

        openText.SetActive(false);

        Debug.Log("Уровень пройден! Дверь открыта.");
    }
}