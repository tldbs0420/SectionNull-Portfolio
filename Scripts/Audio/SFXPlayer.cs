using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    AudioSource audioSource;
    public void Init()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public void PlaySFX(Transform other, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        transform.position = other.position;
        audioSource.Play();
    }

    public AudioSource GetAudioSource() { return audioSource; }
}
