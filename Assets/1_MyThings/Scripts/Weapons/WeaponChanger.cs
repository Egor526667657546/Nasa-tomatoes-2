using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField] private List<UIRegister> uiRegisters;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private Inventory invenotry;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject weaponPanel;

    private bool canChange = true;
    private bool prevCanChange = true;

    public bool CanChange { get => canChange; set => canChange = value; }

    //private void Update()
    //{
    //    if (!canChange)
    //    {
    //        HideUI();
    //    }
    //    if (Input.GetMouseButtonDown(2) && animator.GetBool("onLand") && !uiManager.DeadPanel.activeSelf && canChange)
    //    {
    //        ShowUI();
    //    }
    //    if (Input.GetMouseButtonUp(2) && !uiManager.DeadPanel.activeSelf && canChange)
    //    {
    //        foreach (var uiRegister in uiRegisters)
    //        {
    //            if (uiRegister.IsHovered)
    //            {
    //                uiRegister.PressButton();
    //                break;
    //            }
    //        }
    //        HideUI();
    //    }
    //}

    private void Update()
    {
        if (!canChange && prevCanChange)
        {
            HideUI();
        }
        prevCanChange = canChange;

        if (Input.GetMouseButtonDown(2) && !uiManager.DeadPanel.activeSelf && canChange)
        {
            ShowUI();
        }
        if (Input.GetMouseButtonUp(2) && !uiManager.DeadPanel.activeSelf && canChange)
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
            mainPanel.SetActive(true);
        }
    }
    public void ShowUI()
    {
        Cursor.lockState = CursorLockMode.Confined;
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
        weaponPanel.SetActive(false);
    }
    public void ChangeWeapon(int number) // 0 - up, 1 - right, 2 - down, 3 - left
    {
        Debug.Log("0");
        HideUI();
        invenotry.ChangeWeapon(number);
    }
}
