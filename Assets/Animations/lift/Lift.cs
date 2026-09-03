using UnityEngine;
using System.Collections;

public class Lift : MonoBehaviour
{
    public Animator animator;
    public GameObject openText;

private bool playerNear = false;
    private bool isOpen = false;
    private bool isMoving = false;

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
            playerNear = false;
            openText.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerNear && !isMoving && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            isMoving = true;

            animator.SetBool("Open", isOpen);

            openText.SetActive(false);

            StartCoroutine(UnlockAfterTime());
        }
    }

    private IEnumerator UnlockAfterTime()
    {
        yield return new WaitForSeconds(6f);
        isMoving = false;

        if (playerNear)
        {
            openText.SetActive(true);
        }
    }


}
