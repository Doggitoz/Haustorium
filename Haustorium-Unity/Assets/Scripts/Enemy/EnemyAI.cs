using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyAI : MonoBehaviour
{
    //Public method for enemy controllers to call on
    public bool CheckForPlayer()
    {
        if (!initialized)
            Debug.LogWarning("EnemyAI script on object " + gameObject.name + " has not been initialized");
        return (HasLOS() && PlayerInRange);
    }

    //Raycast from transform to player. If nothing in between, return true
    bool HasLOS()
    {
        //use sightRange to raycast size
        return false;
    }

    SphereCollider sphere;
    bool initialized = false;
    float sightRange = 1f;
    public bool PlayerInRange { get; private set; }

    public void Init(float radius, float range)
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
