using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider EffectsSlider;
    public Slider UISlider;
    public AudioClip Click;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SaveData data = SaveData.DATA;
        MasterSlider.value = data.mem.MasterVolume;
        MusicSlider.value = data.mem.MusicVolume;
        EffectsSlider.value = data.mem.EffectsVolume;
        UISlider.value = data.mem.UIVolume;
    }

    public void ReturnToMainMenu()
    {
        GameManager.GM.SetState(GameState.Menu);
    }

    public void ExitGame()
    {
        SaveData.DATA.SaveToJson();
        Application.Quit();
    }

    public void ResumeGame()
    {
        GameManager.GM.SetPause(false);
    }

    public void MasterSliderUpdate()
    {
        AudioManager.AM.SetMasterVolume(MasterSlider.value);
    }
    public void MusicSliderUpdate()
    {
        AudioManager.AM.SetMusicVolume(MusicSlider.value);
    }
    public void EffectsSliderUpdate()
    {
        AudioManager.AM.SetEffectsVolume(EffectsSlider.value);
    }

    public void UISliderUpdate()
    {
        AudioManager.AM.SetUIVolume(UISlider.value);
    }

}
