using UnityEngine;

public class FireballAbility : Ability
{
    [SerializeField] private GameObject fireball;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Activate();
        }
    }
    public override void Activate()
    {
        Vector3 curPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(fireball, curPos += transform.forward, transform.rotation);
    }
}
