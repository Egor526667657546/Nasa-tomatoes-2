using UnityEngine;

public class CostilForAnimation : MonoBehaviour
{
    [SerializeField] private CameraMove camera1;

    private void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(camera1.CameraPitch - 90, 0, 90);
    }
}
