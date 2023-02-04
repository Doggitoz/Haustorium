using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject TitleScreen;
    public GameObject OptionsScreen;
    public GameObject CreditsScreen;
    public GameObject WarningScreen;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider EffectsSlider;
    public Slider UISlider;
    public AudioClip Click;

    private void Start()
    {
        SaveData data = SaveData.DATA;
        MasterSlider.value = data.mem.MasterVolume;
        MusicSlider.value = data.mem.MusicVolume;
        EffectsSlider.value = data.mem.EffectsVolume;
        UISlider.value = data.mem.UIVolume;
    }

    public void OpenOptionsMenu()
    {
        ClickSound();
        TitleScreen.SetActive(false);
        OptionsScreen.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        ClickSound();
        TitleScreen.SetActive(true);
        OptionsScreen.SetActive(false);
    }

    public void OpenCreditsMenu()
    {
        ClickSound();
        TitleScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void CloseCreditsMenu()
    {
        ClickSound();
        TitleScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }

    public void StartGame()
    {
        ClickSound();
        GameManager.GM.ChangeState(GameState.Playing);
    }

    public void ExitGame()
    {
        ClickSound();
        SaveData.DATA.SaveToJson();
        Application.Quit();
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

    public void ClickSound()
    {
        AudioManager.AM.PlayUI(Click);
    }

    public void ResetDataButton()
    {
        ClickSound();
        WarningScreen.SetActive(true);
    }

    public void AcceptResetData()
    {
        ClickSound();
        SaveData.DATA.ClearData();
        Debug.Log("Reset data...");
        WarningScreen.SetActive(false);
    }

    public void CancelResetData()
    {
        ClickSound();
        WarningScreen.SetActive(false);
    }
}
