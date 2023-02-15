using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameState StartingState;
    GameState _currState;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] DeathScreen ds;

    public bool canPause = true;
    public bool isPaused { get; private set; } = false;

    [SerializeField] AudioClip mainMenuTheme;
    [SerializeField] AudioClip ambientMusic;

    [Header("Flags for escaping")]
    //Power cell, scrubber, weed-ex canister
    public bool hasPowerCell = false;
    public bool hasScrubber = false;
    public bool hasWeedEx = false;

    public UnityEvent OnGamePause { get; private set; }

    #region GameManager Singleton
    static private GameManager gm;
    static public GameManager GM { get { return gm; } }

    void CheckManagerInScene()
    {

        if (gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void Awake()
    {
        CheckManagerInScene();
        OnGamePause = new UnityEvent();
    }

    void Start()
    {
        SetPause(false);
        SetState(StartingState);
    }

    #region GameState Management

    public GameState GetState() { return _currState; }

    public void SetState(GameState newState)
    {
        _currState = newState;

        switch (_currState)
        {
            case GameState.Menu:
                MenuStart();
                break;
            case GameState.Playing:
                PlayingStart();
                break;
            case GameState.Death:
                DeathStart();
                break;
            case GameState.Escaped:
                EscapeStart();
                break;
            case GameState.Intro:
                IntroStart();
                break;
        }

    }
    #endregion

    #region GameState Start Methods

    void MenuStart()
    {
        AudioManager.AM.StopMusic();
        SceneManager.LoadScene(0);
        SetPause(false);
        SetCursor(true);
        AudioManager.AM.PlayMusic(mainMenuTheme);
    }

    void PlayingStart()
    {
        hasPowerCell = false;
        hasScrubber = false;
        hasWeedEx = false;
        AudioManager.AM.StopMusic();

        SceneManager.LoadScene(2);
        SetCursor(false);
        AudioManager.AM.PlayMusic(ambientMusic);
    }

    void DeathStart()
    {
        SetCursor(true);
        ds.EnableScreen();
    }

    void EscapeStart()
    {
        SetCursor(true);
        //SceneManager.LoadScene(1);
        SetState(GameState.Menu);
    }

    void IntroStart()
    {
        AudioManager.AM.StopMusic();
        SceneManager.LoadScene(1);
    }

    #endregion

    #region Pausing

    public void SetPause(bool val)
    {
        if (val == isPaused) return;
        isPaused = val;
        OnGamePause.Invoke();
        pauseMenu.SetActive(val);

        SetCursor(val);
    }

    #endregion

    #region Game Flags

    public void CollectPowerCell()
    {
        hasPowerCell = true;
        //Logic to enable a UI element
    }

    public bool UsePowerCell()
    {
        if (!hasPowerCell) return false;

        //Logic to disable UI element

        return true;
    }

    public void CollectScrubber()
    {
        hasScrubber = true;
        //Logic to enable a UI element
    }

    public bool UseScrubber()
    {
        if (!hasScrubber) return false;

        //Logic to disable UI element

        return true;
    }

    public void CollectWeedEx()
    {
        hasWeedEx = true;
        //Logic to enable a UI element
    }

    public bool UseWeedEx()
    {
        if (!hasWeedEx) return false;

        //Logic to disable UI element

        return true;
    }

    #endregion

    public void SetCursor(bool val)
    {
        if (val)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

[System.Serializable]
public enum GameState
{
    Menu, Intro, Playing, Death, Escaped
}