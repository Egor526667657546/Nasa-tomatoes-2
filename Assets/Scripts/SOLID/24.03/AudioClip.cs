using System.Collections.Generic;
using UnityEngine;

public abstract class AudioClip : MonoBehaviour
{
    [SerializeField] protected List<UnityEngine.AudioClip> clips;

    public abstract void PlayClip(AudioSource audioSource);
}
