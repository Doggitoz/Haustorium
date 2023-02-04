using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalInput : MonoBehaviour
{
    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!GameManager.GM.canPause) return;

        if (GameManager.GM.GetState() == GameState.Playing)
        {
            GameManager.GM.SetState(GameState.Paused);
        }
        else if (GameManager.GM.GetState() == GameState.Paused)
        {
            GameManager.GM.SetState(GameState.Playing);
        }
    }
}
