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
    [SerializeField] Component enemyBehaviorScript;
    IEnemyBehavior _enemyBehavior;

    // Collider to detect bullets
    [SerializeField] EnemyHurtBox _hurtBox;

    // Component used for plant's detection area. Set reference in inspector.
    [SerializeField] SphereCollider _visionSphere;
    
    #region Raycast Fields

    [Header("Raycast Variables")]
    [Tooltip("Debug for LOS cast")] [SerializeField] GameObject debugTarget;

    // Raycast layermask
    [SerializeField] LayerMask _lineOfSight;

    // Raycast source
    [SerializeField] GameObject _raycastSource;

    // Raycast target tag
    [SerializeField] string TagName;
     // this isnt set up yet. trying to do fancy editor scripts

    // Raycast length for player detection. Always equal to _visionSphere's radius
    float sightRange = 5f;

    #endregion

    EnemyState _behaviorState;
    GameObject _target;
    float _timeStunned = 0f;

    public bool PlayerInRange { get; private set; }

    [System.Serializable]
    public enum EnemyState
    {
        Idle, Aggro, Stun, Die
    }

    /// <summary>
    /// Return true if the supplied collider belongs to a player we have LOS to
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool isTargetablePlayer(Collider other)
    {
        GameObject target = other.gameObject;
        return (target.CompareTag("Player") && HasLOSTo(target));
    }

    void handleGetShot(EnemyHurtBox hurtBox)
    {
        _timeStunned = _enemyBehavior.stunDuration;
    }

    /// <summary>
    /// Raycast to target and return true if we hit any part of it, false if we hit anything else
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    bool HasLOSTo(GameObject target)
    {
        //use sightRange to raycast size
        RaycastHit hit;

        if (Physics.Raycast(_raycastSource.transform.position, target.transform.position - _raycastSource.transform.position, out hit, sightRange, _lineOfSight)) {
            return hit.collider.gameObject == target;
            /*
            if(hit.collider.gameObject.CompareTag(TagName))
            {
                return true;
            }
            */
        }
        else return false;
    }

    private void Awake()
    {
        //_enemyBehavior = enemyBehaviorScript as IEnemyBehavior;
        _enemyBehavior = GetComponent<IEnemyBehavior>();
        if ( _enemyBehavior == null ) { print("Enemy Behavior is missing!"); }
    }

    private void Start()
    {
        sightRange = _visionSphere.radius;
        _hurtBox.onShot += handleGetShot;
        //sphere.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.CompareTag("Projectile"))
        {
            _timeStunned = _enemyBehavior.stunDuration;
            //_timeStunned = 10;
        }
        else*/
        if (isTargetablePlayer(other))
        {
            PlayerInRange = true;
            _target = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!PlayerInRange && isTargetablePlayer(other))
        {
            PlayerInRange = true;
            _target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInRange = false;
            _target = null;
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
        //HasLOSTo(debugTarget);
    }

    // BEHAVIOR STATES
    private void IdleState()
    {
        if (_timeStunned > 0f)
        {
            _behaviorState = EnemyState.Stun;
            _enemyBehavior.Stun();
            //print("Stunned!");
        }
        else if (PlayerInRange)
        {
            _behaviorState = EnemyState.Aggro;
            _enemyBehavior.Attack(_target);
            //print("Aggro!");
        }
    }

    private void AggroState()
    {
        if (_timeStunned > 0f)
        {
            _behaviorState = EnemyState.Stun;
            _enemyBehavior.Stun();
            //print("Stunned!");
        }
        if (_target == null || !PlayerInRange)
        {
            _behaviorState = EnemyState.Idle;
            _enemyBehavior.Idle();
            //print("Idling.");
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
