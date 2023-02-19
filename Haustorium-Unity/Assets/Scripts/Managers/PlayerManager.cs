using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region PlayerManager Singleton
    static private PlayerManager instance;
    static public PlayerManager Instance { get { return instance; } }

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

    public GameObject Player { get; private set; }
    public PlayerController Controller { get; private set; }
    public Transform Transform { get; private set; }
    [SerializeField] GameObject PlayerPrefab;

    private Vector3 _spawnPos;

    private void Awake()
    {
        CheckManagerInScene();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnload;
    }

    private void OnSceneUnload(Scene scene)
    {
        _spawnPos = Vector3.zero;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnPlayer(_spawnPos);
    }

    public void SpawnPlayer(Vector3 pos)
    {
        if (GameManager.Instance.GetState() != GameState.Playing) return;
        Debug.Log("Spawning player");
        if (Player != null)
        {
            Destroy(Player);
        }
        Player = Instantiate(PlayerPrefab);
        PlayerPrefab.transform.position = pos;
        Controller = Player.GetComponent<PlayerController>();
        Player.name = "Player";
        Debug.Log(Player);
    }

    public void SetPlayerSpawn(Vector3 SpawnPos)
    {
        _spawnPos = SpawnPos;
    }

}
