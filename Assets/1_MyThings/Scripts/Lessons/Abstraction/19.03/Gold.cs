using UnityEngine;

public class Gold : Resource
{
    [SerializeField] private GoldSO goldSO;
    private void Awake()
    {
        name = goldSO.name;
    }
    public override void Collect()
    {
        goldSO.ammount++;
        ammount = goldSO.ammount;
        Debug.Log($"+ 1 золото");
        Destroy(gameObject);
    }
}
