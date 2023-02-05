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

    public bool canPause = true;
    public bool isPaused { get; private set; } = false;

    [SerializeField] AudioClip mainMenuTheme;
    [SerializeField] AudioClip ambientMusic;

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
        AudioManager.AM.StopMusic();
        SceneManager.LoadScene(1);
        SetCursor(false);
    }

    void DeathStart()
    {

    }

    void EscapeStart()
    {
        SetCursor(true);
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
    Menu, Playing, Death, Escaped
}