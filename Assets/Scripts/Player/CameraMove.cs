using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;

    public static Action OnPause;

    private bool canRotate = true;
    private float axisCamera;

    private void Start()
    {
        OnPause += ChangeRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (canRotate)
        {
            RotateCamera();
        }

    }
    public void RotateCamera()
    {
        float X = Input.GetAxis("Mouse X") * sensitivityX;
        float Y = Input.GetAxis("Mouse Y") * sensitivityY;

        body.Rotate(Vector3.up, X);
        axisCamera += -Y;
        axisCamera = Mathf.Clamp(axisCamera, -30, 55);
        transform.localRotation = Quaternion.Euler(axisCamera, 0, 0);
        //transform.Rotate(Vector3.right, Y);

    }
    private void ChangeRotation()
    {
        canRotate = !canRotate;
    }
    private void OnDestroy()
    {
        OnPause -= ChangeRotation;
    }
}
