using UnityEngine;

public class Clips1 : AudioClip
{
    public override void PlayClip(AudioSource audioSource)
    {
        audioSource.clip = clips[Random.Range(0, clips.Count - 1)];
    }
}
