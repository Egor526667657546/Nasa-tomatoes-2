using UnityEngine;

public class DashAbility : Ability
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Activate();
        }
    }
    public override void Activate()
    {
        
    }
}
