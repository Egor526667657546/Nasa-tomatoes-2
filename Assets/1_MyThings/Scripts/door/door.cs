using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public GameObject openText;

    private bool playerNear = false;

    private void Start()
    {
        openText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            openText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Open", false);
            playerNear = false;
            openText.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("Open", true);
            openText.SetActive(false);
        }
    }
}