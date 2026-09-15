using UnityEngine;

public class DelieverObjects : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;

    private int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerInventory.DeliverThing())
            {
                Debug.Log("Delievered");
                count += 1;
            }
        }
    }
}
