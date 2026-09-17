using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> explosions;

    public void PlayExplosion()
    {
        foreach (var i in explosions)
        {
            if (i.gameObject.transform.parent.gameObject.activeInHierarchy)
            {
                i.Play();
            }
        }
    }
}