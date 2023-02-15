using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class PlayerDetection : MonoBehaviour
{
    SphereCollider sphere;
    float enemyRange = 1f;
    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        sphere.isTrigger = true;
    }

    public void Init(float range)
    {
        enemyRange = range;
        sphere.transform.localScale = Vector3.one * range;
    }

    public bool PlayerInRange { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}
