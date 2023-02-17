using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Developer Mode")]
    [SerializeField] bool DeveloperMode;
    [SerializeField] GameState StartingState;
    [SerializeField] bool SceneOverride;

    [Header("Global UIs")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] DeathScreen ds;

    [Header("Audio Clips")]
    [SerializeField] AudioClip mainMenuTheme;
    [SerializeField] AudioClip ambientMusic;

    [Header("Flags for escaping")]
    //Power cell, scrubber, weed-ex canister
    public bool hasPowerCell = false;
    public bool hasScrubber = false;
    public bool hasWeedEx = false;

    GameState _currState;

    [HideInInspector] public bool canPause = true;
    [HideInInspector] public bool isPaused { get; private set; } = false;
    public UnityEvent OnGamePause { get; private set; }

    #region GameManager Singleton
    static private GameManager instance;
    static public GameManager Instance { get { return instance; } }

    void CheckManagerInScene()
    {

        if (instance == null)
        {
            instance = this;
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
        //If developer mode enabled, do fun developer mode stuff!
        if (DeveloperMode)
        {
            SetState(StartingState);
        }
        else
        {
            SetState(GameState.Menu);
        }
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
        AudioManager.Instance.StopMusic();
        ChangeScene(0);
        SetPause(false);
        SetCursor(true);
        AudioManager.Instance.PlayMusic(mainMenuTheme);
    }

    void PlayingStart()
    {
        hasPowerCell = false;
        hasScrubber = false;
        hasWeedEx = false;
        AudioManager.Instance.StopMusic();
        ChangeScene(2);
        SetCursor(false);
        AudioManager.Instance.PlayMusic(ambientMusic);
        if (!SceneOverride || !DeveloperMode)
            PlayerManager.Instance.SpawnPlayer(new Vector3(19f, 1f, -16f));
        else
            PlayerManager.Instance.SpawnPlayer(Vector3.zero); //really stupid!!!
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
        AudioManager.Instance.StopMusic();
        ChangeScene(1);
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

    public void ChangeScene(int sceneIndex)
    {
        if (DeveloperMode && SceneOverride)
            return; // This is kinda silly but idrc since it has no effect on general build gameplay
        SceneManager.LoadScene(sceneIndex);
    }
}

[System.Serializable]
public enum GameState
{
    Menu, Intro, Playing, Death, Escaped
}