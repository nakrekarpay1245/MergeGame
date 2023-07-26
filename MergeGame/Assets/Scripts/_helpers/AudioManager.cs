using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField]
    private int maximumSoundCount = 10;
    [SerializeField]
    private float masterVolume = 1f;
    [SerializeField]
    private bool isAudioSourceMuted = false;

    [SerializeField]
    private List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField]
    private List<Sound> soundList = new List<Sound>();

    //[SerializeField]
    //private Slider musicSlider;
    //[SerializeField]
    //private Slider soundSlider;
    [SerializeField]
    private Toggle soundToggle;

    private void Awake()
    {
        // AudioSource componentlerini oluþtur
        for (int i = 0; i < maximumSoundCount; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(newSource);
        }
    }

    private void Start()
    {
        //if (soundSlider != null)
        //{
        //    masterVolume = SaveLoadManager.singleton.GetSoundVolume();
        //    //musicSlider.value = masterVolume;
        //    soundSlider.value = masterVolume;
        //}
        if (soundToggle != null)
        {
            //isAudioSourceMuted = SaveLoadManager.singleton.GetSoundMuted();
            soundToggle.isOn = !isAudioSourceMuted;
        }
    }

    /// <summary>
    /// Plays an audio clip with the given name, volume, and loop setting, using an available AudioSource.
    /// If no AudioSource is available, logs a warning message and returns without playing the
    /// </summary>
    /// <param name="clipName"></param>
    /// <param name="volume"></param>
    /// <param name="loop"></param>
    public void PlaySound(string clipName, float volume = 1f, bool loop = false)
    {
        // Find audio source which is not playing
        AudioSource activeSource = null;
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                activeSource = audioSources[i];
                break;
            }
        }

        if (activeSource == null)
        {
            Debug.LogWarning("There is no any other audioSource");
            return;
        }

        for (int i = 0; i < soundList.Count; i++)
        {
            if (clipName == soundList[i].Name)
            {
                activeSource.mute = isAudioSourceMuted;

                activeSource.clip = soundList[i].Clip;
                activeSource.volume = masterVolume * soundList[i].Volume;
                activeSource.loop = soundList[i].Loop;
                activeSource.Play();
            }
        }
    }

    /// <summary>
    /// Plays an audio clip with the given clip reference, volume, and loop setting, using an available AudioSource.
    /// If no AudioSource is available, logs a warning message and returns without playing the   
    /// /// </summary>
    /// <param name="clip"></param>
    /// <param name="volume"></param>
    /// <param name="loop"></param>
    public void PlaySound(AudioClip clip, float volume = 1f, bool loop = false)
    {
        // Find audio source which is not playing
        AudioSource activeSource = null;
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                activeSource = audioSources[i];
                break;
            }
        }

        if (activeSource == null)
        {
            Debug.LogWarning("There is no any other audioSource");
            return;
        }

        activeSource.mute = isAudioSourceMuted;

        activeSource.clip = clip;
        activeSource.volume = masterVolume * volume;
        activeSource.loop = loop;
        activeSource.Play();
    }

    /// <summary>
    /// This function stops all audio playback from all audio sources in the audioSources list
    /// </summary>
    public void StopAllSounds()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].Stop();
        }
    }

    //public void SetMasterVolume(float volume)
    //{
    //    masterVolume = volume;
    //    for (int i = 0; i < audioSources.Count; i++)
    //    {
    //        bool isAudioSourcePlaying = audioSources[i].isPlaying;
    //        audioSources[i].volume = audioSources[i].volume * volume;

    //        if (isAudioSourcePlaying)
    //            audioSources[i].Play();
    //    }

    //    //SaveLoadManager.singleton.SetMusicVolume(masterVolume);
    //    SaveLoadManager.singleton.SetSoundVolume(masterVolume);
    //}

    public void ToggleMute(bool toogleValue)
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].mute = !audioSources[i].mute;
            isAudioSourceMuted = audioSources[i].mute;
        }

        //SaveLoadManager.singleton.SetSoundMuted(audioSources[0].mute);
        //SaveLoadManager.singleton.SetMusicMuted(audioSources[0].mute);
    }
}
