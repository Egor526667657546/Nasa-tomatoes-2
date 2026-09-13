using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private Animator itemAnimator;

    public void SpawnItem()
    {
        if (item != null)
        {
            item.SetActive(true);
        }

        if (itemAnimator != null)
        {
            itemAnimator.Play(0);
        }
    }
}