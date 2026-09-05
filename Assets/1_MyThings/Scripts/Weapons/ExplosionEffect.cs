using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private List<ParticleSystem> explosions;

    public void PlayExplosion()
    {
        int count = 0;
        foreach (var i in explosions)
        {
            if (i.gameObject.activeSelf)
            {
                explosions[count].Play();
            }
            count++;
        }
    }
}
