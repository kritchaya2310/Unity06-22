using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioClip[] sound;
    AudioSource _audioSource;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(5);
    }

    public void PlaySound2()
    {
        AudioManager.instance.PlaySFX(6);
    }
}
