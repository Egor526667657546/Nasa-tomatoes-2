using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> explosions;

    public void PlayExplosion()
    {
        foreach (ParticleSystem explosion in explosions)
        {
            if (explosion != null)
            {
                explosion.Play();

            }
        }
    }
}