using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public float damage;
    public float attackSpeed;
    public int cartridges;
    public float reloadSpeed;
    public string type;
}
