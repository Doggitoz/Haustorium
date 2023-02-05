using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject screen;
    // Start is called before the first frame update
    void Start()
    {
        screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableScreen()
    {
        screen.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        GameManager.GM.SetState(GameState.Menu);
        screen.SetActive(false);
    }

    public void RetryGame()
    {
        GameManager.GM.SetState(GameState.Playing);
        screen.SetActive(false);
    }
}
