using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float cooldown;
    public abstract void Activate();

    public virtual IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
    }
}