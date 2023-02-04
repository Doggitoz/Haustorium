using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource EffectsSource;
    [SerializeField] AudioSource UISource;

    float MasterVolume = 1.0f;
    float MusicVolume = 1.0f;
    float UIVolume = 1.0f;
    float EffectVolume = 1.0f;

    #region AudioManager Singleton
    static private AudioManager am;
    static public AudioManager AM { get { return am; } }

    void CheckAudioManagerIsInScene()
    {

        if (am == null)
        {
            am = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void Awake()
    {
        CheckAudioManagerIsInScene();
    }

    #region Play/Stop Audio Public Methods
    // I would like to update this to accomodate for multiple effects overlapping. This could potentially be accomplished by multiple audio sources on each
    // with some variant of a queue to determine which audio has a priority to stop if all playing. Doesn't seem too hard, just needs trial and error.

    public void PlayMusic(AudioClip clip, bool fadeOut = true, bool fadeIn = false)
    {
        MusicSource.Stop();
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public void StopMusic(bool fadeOut = true)
    {
        MusicSource.Stop();
    }

    public void PlayUI(AudioClip clip)
    {
        UISource.Stop();
        UISource.clip = clip;
        UISource.Play();
    }

    public void StopUI()
    {
        UISource.Stop();
    }

    public void PlayEffect(AudioClip clip)
    {
        EffectsSource.Stop();
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    public void StopEffect()
    {
        EffectsSource.Stop();
    }
    #endregion

    #region Update Volume Public Methods
    public void SetMasterVolume(float volume)
    {
        MasterVolume = volume;
        SaveData.DATA.mem.MasterVolume = volume;
        MusicSource.volume = MasterVolume * MusicVolume;
        UISource.volume = MasterVolume * UIVolume;
        EffectsSource.volume = MasterVolume * EffectVolume;
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        SaveData.DATA.mem.MusicVolume = volume;
        MusicSource.volume = MasterVolume * MusicVolume;
    }

    public void SetUIVolume(float volume)
    {
        UIVolume = volume;
        SaveData.DATA.mem.UIVolume = volume;
        UISource.volume = MasterVolume * UIVolume;
    }

    public void SetEffectsVolume(float volume)
    {
        EffectVolume = volume;
        SaveData.DATA.mem.EffectsVolume = volume;
        EffectsSource.volume = MasterVolume * EffectVolume;
    }
    #endregion

    #region Getters for Local Volume Control

    public float GetGlobalEffectsVolume()
    {
        return MasterVolume * EffectVolume;
    }

    public float GetGlobalMusicVolume()
    {
        return MasterVolume * MusicVolume;
    }

    public float GetGlobalUIVolume()
    {
        return MasterVolume * UIVolume;
    }

    #endregion

}