using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private AnimManager animManager;
    [SerializeField] private LayerMask layer;
    bool canAnim = true;

    private void Update()
    {
        if (canAnim)
        {
            ShootLaser();
        }
    }
    private void ShootLaser()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 30f, layer))
        {
            animManager.StartAnim();
            canAnim = false;
        }
    }
}
