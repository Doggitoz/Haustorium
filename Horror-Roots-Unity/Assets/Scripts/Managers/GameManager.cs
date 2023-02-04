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
        ChangeState(StartingState);
    }

    
    void Update()
    {
        
    }

    #region GameState Management

    public GameState GetState() { return _currState; }

    public void ChangeState(GameState newState)
    {
        _currState = newState;

        switch (_currState)
        {
            case GameState.Menu:
                break;
            case GameState.Playing:
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

    #endregion

}

[System.Serializable]
public enum GameState
{
    Menu, Playing
}