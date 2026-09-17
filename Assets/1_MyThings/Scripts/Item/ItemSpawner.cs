using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private Animator itemAnimator;

    public void SpawnItem()
    {
        if (item == null || itemAnimator == null)
            return;

        item.SetActive(true);
        itemAnimator.SetTrigger("Start");
    }
}