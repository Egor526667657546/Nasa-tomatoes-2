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
    private float camRotCeiling = -30f;
    private float camRotFloor = 55f;
    private float axisCamera;

    public float CameraPitch
    {
        get => axisCamera;
    }

    public float CamRotCeiling { get => camRotCeiling; set => camRotCeiling = value; }
    public float CamRotFloor { get => camRotFloor; set => camRotFloor = value; }
    public bool CanRotate { get => canRotate; set => canRotate = value; }

    private void Start()
    {
        OnPause += ChangeRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (CanRotate)
        {
            RotateCamera();
        }
    }

    public void RotateCamera()
    {
        float X = Input.GetAxis("Mouse X") * sensitivityX;
        float y = Input.GetAxis("Mouse Y") * sensitivityY;

        body.Rotate(Vector3.up, X);
        axisCamera += -y;
        axisCamera = Mathf.Clamp(axisCamera, CamRotCeiling, CamRotFloor);

        transform.localRotation = Quaternion.Euler(0, -90, -90);
    }

    private void ChangeRotation()
    {
        CanRotate = !CanRotate;
    }

    private void OnDestroy()
    {
        OnPause -= ChangeRotation;
    }
}