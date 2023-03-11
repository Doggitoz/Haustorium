using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle player detection and enemy AI states
/// Spherecast for player, then raycast for line of sight.
/// States are: Idle, Aggro, and Stun.
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class EnemyAI : MonoBehaviour
{
    //Public method for enemy controllers to call on
    public bool CheckForPlayer()
    {
        if (!initialized)
            Debug.LogWarning("EnemyAI script on object " + gameObject.name + " has not been initialized");
        //return (HasLOS() && PlayerInRange);
        return false; // TODO FIX ME
    }

    /// <summary>
    /// Raycast to target and return true if we hit any part of it, false if we hit anything else
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    bool HasLOSTo(GameObject target)
    {
        //use sightRange to raycast size
        return false;
    }

    SphereCollider sphere;
    bool initialized = false;
    float sightRange = 1f;
    public bool PlayerInRange { get; private set; }

    public void Init(float radius = 1, float range = 2)
    {
        initialized = true;
        sightRange = range;
        sphere.transform.localScale = Vector3.one * radius;
    }

    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        sphere.isTrigger = true;
    }
    
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
