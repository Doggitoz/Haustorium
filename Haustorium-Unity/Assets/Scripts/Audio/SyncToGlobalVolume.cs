using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncToGlobalVolume : MonoBehaviour
{
    [SerializeField] AudioType audioType;
    [SerializeField] AudioSource audioSource;
    [SerializeField][Range(0f, 1f)] float localVolume = 1f;

    private void Start()
    {
        UpdateVolume();
        AudioManager.AM.UpdateAudioControls.AddListener(UpdateVolume);
    }

    void UpdateVolume()
    {
        switch(audioType)
        {
            case AudioType.Effects:
                audioSource.volume = AudioManager.AM.GetGlobalEffectsVolume() * localVolume;
                break;
            case AudioType.UI:
                audioSource.volume = AudioManager.AM.GetGlobalUIVolume() * localVolume;
                break;
            case AudioType.Music:
                audioSource.volume = AudioManager.AM.GetGlobalMusicVolume() * localVolume;
                break;
        }
    }

    private enum AudioType 
    { 
        Effects, UI, Music
    }
}
