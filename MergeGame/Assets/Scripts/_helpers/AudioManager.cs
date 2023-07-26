using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField]
    private int _maximumSoundCount = 10;
    [SerializeField]
    private float _masterVolume = 1f;
    [SerializeField]
    private bool _isAudioSourceMuted = false;

    [SerializeField]
    private List<AudioSource> _audioSources = new List<AudioSource>();
    [SerializeField]
    private List<Sound> _soundList = new List<Sound>();

    //[SerializeField]
    //private Slider musicSlider;
    //[SerializeField]
    //private Slider soundSlider;
    [SerializeField]
    private Toggle _soundToggle;

    private void Awake()
    {
        // AudioSource componentlerini oluþtur
        for (int i = 0; i < _maximumSoundCount; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(newSource);
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
        if (_soundToggle != null)
        {
            //isAudioSourceMuted = SaveLoadManager.singleton.GetSoundMuted();
            _soundToggle.isOn = !_isAudioSourceMuted;
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
        for (int i = 0; i < _audioSources.Count; i++)
        {
            if (!_audioSources[i].isPlaying)
            {
                activeSource = _audioSources[i];
                break;
            }
        }

        if (activeSource == null)
        {
            Debug.LogWarning("There is no any other audioSource");
            return;
        }

        for (int i = 0; i < _soundList.Count; i++)
        {
            if (clipName == _soundList[i].Name)
            {
                activeSource.mute = _isAudioSourceMuted;

                activeSource.clip = _soundList[i].Clip;
                activeSource.volume = _masterVolume * _soundList[i].Volume;
                activeSource.loop = _soundList[i].Loop;
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
        for (int i = 0; i < _audioSources.Count; i++)
        {
            if (!_audioSources[i].isPlaying)
            {
                activeSource = _audioSources[i];
                break;
            }
        }

        if (activeSource == null)
        {
            Debug.LogWarning("There is no any other audioSource");
            return;
        }

        activeSource.mute = _isAudioSourceMuted;

        activeSource.clip = clip;
        activeSource.volume = _masterVolume * volume;
        activeSource.loop = loop;
        activeSource.Play();
    }

    /// <summary>
    /// This function stops all audio playback from all audio sources in the audioSources list
    /// </summary>
    public void StopAllSounds()
    {
        for (int i = 0; i < _audioSources.Count; i++)
        {
            _audioSources[i].Stop();
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
        for (int i = 0; i < _audioSources.Count; i++)
        {
            _audioSources[i].mute = !_audioSources[i].mute;
            _isAudioSourceMuted = _audioSources[i].mute;
        }

        //SaveLoadManager.singleton.SetSoundMuted(audioSources[0].mute);
        //SaveLoadManager.singleton.SetMusicMuted(audioSources[0].mute);
    }
}
