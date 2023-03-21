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
    // Enemy behavior component. Set reference in inspector.
    [SerializeField] IEnemyBehavior _enemyBehavior;
    // Component used for plant's detection area. Set reference in inspector.
    [SerializeField] SphereCollider _visionSphere;
    
    // Raycast length for player detection. Always equal to _visionSphere's radius
    float sightRange = 5f;

    EnemyState _behaviorState;
    GameObject _target;
    float _timeStunned = 0f;

    public bool PlayerInRange { get; private set; }

    [System.Serializable]
    public enum EnemyState
    {
        Idle, Aggro, Stun, Die
    }

    // pass a collider, return true if it's a player we have LOS to
    public bool isTargetablePlayer(Collider other)
    {
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

    private void Awake()
    {
        // Set references in inspector so no need to hunt for things on Awake()
        //sphere = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        sightRange = _visionSphere.radius;
        //sphere.isTrigger = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            _timeStunned = _enemyBehavior.stunDuration;
        }
        else if (isTargetablePlayer(other))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!PlayerInRange && isTargetablePlayer(other))
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
            case EnemyState.Die:
                Die();
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
            print("Stunned!");
        }
        else if (PlayerInRange)
        {
            _behaviorState = EnemyState.Aggro;
            _enemyBehavior.Attack(_target);
            print("Aggro!");
        }
    }

    private void AggroState()
    {
        if (_timeStunned > 0f)
        {
            _behaviorState = EnemyState.Stun;
            _enemyBehavior.Stun();
            print("Stunned!");
        }
        if (_target == null || !PlayerInRange)
        {
            _behaviorState = EnemyState.Idle;
            _enemyBehavior.Idle();
            print("Idling.");
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
                print("Aggro!");
            }
            else
            {
                _behaviorState = EnemyState.Idle;
                _enemyBehavior.Idle();
                print("Idle.");
            }
        }
    }

    private void Die()
    {
        // enemyBehavior must destroy gameObject when complelte
        _enemyBehavior.Die();
        print("Ded.");
    }
}
