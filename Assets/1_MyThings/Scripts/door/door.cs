using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private AnimManager animManager;
    public Animator animator;

    private bool levelCompleted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!levelCompleted)
            {
                animator.SetBool("open", true);
                animManager.StartAnim();
            }
        }
    }
    public void LevelCompleted()
    {
        levelCompleted = true;

        animator.SetBool("open", false);
    }
}