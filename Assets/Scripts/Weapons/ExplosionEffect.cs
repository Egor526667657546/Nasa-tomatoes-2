using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;

    public void PlayExplosion()
    {
        explosion.Play();
    }
}
