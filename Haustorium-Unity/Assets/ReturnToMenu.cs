using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    public void MainMenu()
    {
        GameManager.Instance.SetState(GameState.Menu);
    }
}
