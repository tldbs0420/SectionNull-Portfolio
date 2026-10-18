using UnityEngine;

public class UISFXPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip click;

    public void PlayClick()
    {
        audioSource.PlayOneShot(click);
    }
    public AudioSource GetAudioSource() { return audioSource; }
}
