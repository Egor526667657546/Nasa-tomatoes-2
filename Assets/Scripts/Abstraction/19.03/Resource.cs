using Unity.VisualScripting;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    protected string name = "not initialized";
    protected int ammount = 0;
    public abstract void Collect();

    public void ShowAmmount()
    {
        Debug.Log($"{name} має {ammount} одиниць");
    }
}
