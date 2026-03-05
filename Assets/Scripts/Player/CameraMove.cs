using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;

    private float axisCamera;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        RotateCamera();
    }
    public void RotateCamera()
    {
        float X = Input.GetAxis("Mouse X") * sensitivityX;
        float Y = Input.GetAxis("Mouse Y") * sensitivityY;

        body.Rotate(Vector3.up, X);
        axisCamera += -Y;
        axisCamera = Mathf.Clamp(axisCamera, -30, 90);
        transform.localRotation = Quaternion.Euler(axisCamera, 0, 0);
        //transform.Rotate(Vector3.right, Y);

    }
}
