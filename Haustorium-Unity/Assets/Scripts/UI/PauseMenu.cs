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
        SaveData data = SaveData.Instance;
        MasterSlider.value = data.mem.MasterVolume;
        MusicSlider.value = data.mem.MusicVolume;
        EffectsSlider.value = data.mem.EffectsVolume;
        UISlider.value = data.mem.UIVolume;
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.SetState(GameState.Menu);
    }

    public void ExitGame()
    {
        SaveData.Instance.SaveToJson();
        Application.Quit();
    }

    public void ResumeGame()
    {
        GameManager.Instance.SetPause(false);
        SaveData.Instance.SaveToJson();
    }

    public void MasterSliderUpdate()
    {
        AudioManager.Instance.SetMasterVolume(MasterSlider.value);
    }
    public void MusicSliderUpdate()
    {
        AudioManager.Instance.SetMusicVolume(MusicSlider.value);
    }
    public void EffectsSliderUpdate()
    {
        AudioManager.Instance.SetEffectsVolume(EffectsSlider.value);
    }

    public void UISliderUpdate()
    {
        AudioManager.Instance.SetUIVolume(UISlider.value);
    }

}
