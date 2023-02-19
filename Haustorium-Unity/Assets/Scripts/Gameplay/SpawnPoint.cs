using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] bool InitialSpawnPoint = false;

    private void Awake()
    {
        if (InitialSpawnPoint)
        {
            PlayerManager.Instance.SetPlayerSpawn(transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayerManager.Instance.SetPlayerSpawn(transform.position);
    }

}
