//using UnityEngine;

//public class DeliveryZone : MonoBehaviour
//{
//    [SerializeField] private string requiredItemId;
//    [SerializeField] private string playerTag = "Player";

//    private void OnTriggerEnter(Collider other)
//    {
//        if (!other.CompareTag(playerTag)) return;

//        Inventory inventory = other.GetComponent<Inventory>();
//        if (inventory == null) return;

//        if (inventory.HasThing(requiredItemId))
//        {
//            inventory.DeliverThing(requiredItemId);
//            OnDelivered();
//        }
//    }

//    private void OnDelivered()
//    {
//        // сюда: открыть дверь, засчитать квест, проиграть эффект и т.д.
//        Debug.Log($"Доставлен предмет: {requiredItemId}");
//    }
//}
