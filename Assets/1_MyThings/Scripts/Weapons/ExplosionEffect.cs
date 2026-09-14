using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> explosions;

    public void PlayExplosion(int number)
    {
        explosions[number].Play();
        explosions[2].Play();
    }
}