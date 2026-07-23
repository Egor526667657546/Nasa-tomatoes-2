using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<WeaponItem>(out WeaponItem weaponItem))
        {
            PlayerShooting.OnPickUpWeapon(weaponItem.WeaponData);
            Destroy(other.gameObject);
        }
    }
}
