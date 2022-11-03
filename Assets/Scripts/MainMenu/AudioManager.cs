using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource mainMenuMusic, levelMusic;

    public AudioSource[] sfx;

    public void PlayMainMenuMusic()
    {
        mainMenuMusic.volume = 0f;
        mainMenuMusic.DOFade(.3f, 5f); //Fade in

        levelMusic.Stop();
        mainMenuMusic.Play();
    }

    public void PlayLevelMusic()
    {
        if (!levelMusic.isPlaying)
        {
            levelMusic.volume = 0f;
            levelMusic.DOFade(0.1f, 5f); //Fade in

            mainMenuMusic.Stop();
            levelMusic.Play();
        }
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void WalkSound()
    {
        PlaySFX(5);
    }
}
