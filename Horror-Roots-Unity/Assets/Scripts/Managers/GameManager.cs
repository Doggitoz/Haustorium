using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameState StartingState;
    GameState _currState;
    public bool Paused = false;

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
    }

    void Start()
    {
        SetState(StartingState);
    }

    
    void Update()
    {
        
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
            case GameState.Paused:
                PauseStart();
                break;
            case GameState.Death:
                DeathStart();
                break;
        }

    }
    #endregion

    #region GameState Start Methods

    void MenuStart()
    {

    }

    void PlayingStart()
    {

    }

    void PauseStart()
    {

    }

    void DeathStart()
    {

    }

    #endregion

}

[System.Serializable]
public enum GameState
{
    Menu, Playing, Paused, Death
}