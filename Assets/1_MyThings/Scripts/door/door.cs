using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private AnimManager animManager;
    public Animator animator;

    private bool levelCompleted = false;
    private bool canPlayAgain = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!levelCompleted)
            {
                animator.SetBool("open", true);
                Debug.Log(animManager != null);
                Debug.Log(canPlayAgain);
                if (animManager != null && canPlayAgain)
                {
                    canPlayAgain = false;
                    animManager.StartAnim();
                }
            }
        }
    }
    public void LevelCompleted()
    {
        levelCompleted = true;

        animator.SetBool("open", false);
    }
}