using Assets.Scripts.Enemy;
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
    [SerializeField] IEnemyBehavior _enemyBehavior;
    EnemyState _behaviorState;
    GameObject _target;
    float _timeStunned = 0f;

    SphereCollider sphere;
    bool initialized = false;
    float sightRange = 1f;
    public bool PlayerInRange { get; private set; }

    [System.Serializable]
    public enum EnemyState
    {
        Idle, Aggro, Stun
    }

    // FIX ME! Public method for enemy controllers to call on
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

    public void Init(float radius = 1, float range = 2)
    {
        initialized = true;
        sightRange = range;
        sphere.transform.localScale = Vector3.one * radius;
        _behaviorState = EnemyState.Idle;
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
        else if (other.gameObject.CompareTag("Projectile"))
        {
            _timeStunned = _enemyBehavior.stunDuration;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }

    /// <summary>
    /// Execute state machine
    /// </summary>
    private void Update()
    {
        switch (_behaviorState)
        {
            case EnemyState.Idle:
                IdleState();
                break;
            case EnemyState.Aggro:
                AggroState();
                break;
            case EnemyState.Stun:
                StunState();
                break;
        }
    }

    // BEHAVIOR STATES
    private void IdleState()
    {
        if (_timeStunned > 0f)
        {
            _behaviorState = EnemyState.Stun;
            _enemyBehavior.Stun();
        }
        else if (PlayerInRange)
        {
            _behaviorState = EnemyState.Aggro;
            _enemyBehavior.Attack(_target);
        }
    }

    private void AggroState()
    {
        if (_timeStunned > 0f)
        {
            _behaviorState = EnemyState.Stun;
            _enemyBehavior.Stun();
        }
        if (_target == null || !PlayerInRange)
        {
            _behaviorState = EnemyState.Idle;
            _enemyBehavior.Idle();
        }
    }

    private void StunState()
    {
        _timeStunned -= Time.deltaTime;
        if (_timeStunned < 0f)
        {
            if (PlayerInRange)
            {
                _behaviorState = EnemyState.Aggro;
                _enemyBehavior.Attack(_target);
            }
            else
            {
                _behaviorState = EnemyState.Idle;
                _enemyBehavior.Idle();
            }
        }
    }
}
