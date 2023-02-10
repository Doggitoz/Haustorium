using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        CheckManagerInScene();
    }

    public void SpawnPlayer(Vector3 SpawnPos)
    {
        if (Player != null)
        {
            Destroy(Player);
        }
        Player = Instantiate(PlayerPrefab);
        PlayerPrefab.transform.position = SpawnPos;
        Controller = Player.GetComponent<PlayerController>();
    }

}
