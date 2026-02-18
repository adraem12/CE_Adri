using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource musicSource;
    public AudioSource effectsSource;
    public AudioClip[] musicAudioClips;
    public AudioClip[] effectAudioClips;

    private void Awake()
    {
        instance = this;
    }

    public void SetMusic(int clip) //Change music clip
    {
        musicSource.clip = musicAudioClips[clip];
        musicSource.Play();
        if (clip == 3)
        {
            musicSource.loop = false;
            StartCoroutine(PlayNextClip(4));
        }
    }

    public void SetEffects(int clip) //Change effect clip
    {
        effectsSource.clip = effectAudioClips[clip];
        effectsSource.Play();
    }

    IEnumerator PlayNextClip(int clip) //Play new clip after end
    {
        yield return new WaitUntil(() => !musicSource.isPlaying);
        musicSource.loop = true;
        musicSource.clip = musicAudioClips[clip];
        musicSource.Play();
    }
}