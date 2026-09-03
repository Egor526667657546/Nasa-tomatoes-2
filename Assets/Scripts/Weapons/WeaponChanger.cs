using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField] private List<UIRegister> uiRegisters;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private Inventory invenotry;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject weaponPanel;

    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(2) && animator.GetBool("onLand"))
        {
            ShowUI();
        }
        if (Input.GetMouseButtonUp(2))
        {
            foreach (var uiRegister in uiRegisters)
            {
                if (uiRegister.IsHovered)
                {
                    uiRegister.PressButton();
                    break;
                }
            }
            HideUI();
        }
    }
    public void ShowUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        cameraMove.CanRotate = false;
        mainPanel.SetActive(false);
        weaponPanel.SetActive(true);
    }
    public void HideUI()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraMove.CanRotate = true;
        mainPanel.SetActive(true);
        weaponPanel.SetActive(false);
    }
    public void ChangeWeapon(int number) // 0 - up, 1 - right, 2 - down, 3 - left
    {
        HideUI();
        invenotry.ChangeWeapon(number);
    }
}
