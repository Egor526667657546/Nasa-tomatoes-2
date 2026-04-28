using UnityEngine;

public class Wood : Resource
{
    [SerializeField] private WoodSO woodSO;
    private void Awake()
    {
        name = woodSO.name;
    }
    public override void Collect()
    {
        woodSO.ammount++;
        ammount = woodSO.ammount;
        Debug.Log($"+ 1 деревина");
        Destroy(gameObject);
    }
}
