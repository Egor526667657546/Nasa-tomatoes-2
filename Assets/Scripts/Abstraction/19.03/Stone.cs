using UnityEngine;

public class Stone : Resource
{
    [SerializeField] private StoneSO stoneSO;
    private void Awake()
    {
        name = stoneSO.name;
    }
    public override void Collect()
    {
        stoneSO.ammount++;
        ammount = stoneSO.ammount;
        Debug.Log($"+ 1 каміння");
        Destroy(gameObject);
    }
}
