using UnityEngine;
using UnityEngine.UI;

public class AudioManager : SingleTon<AudioManager>
{
    public Slider BGMSlider;
    public Slider SFXSlider;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    void Update()
    {
        bgmSource.volume = BGMSlider.value;
        sfxSource.volume = SFXSlider.value;
    }

    public void PlaySFX(AudioClip sfxCilp)
    {
        sfxSource.PlayOneShot(sfxCilp);
    }
}
