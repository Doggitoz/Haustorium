using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class ShooterPolypBehavior : MonoBehaviour, IEnemyBehavior
{
    // Objects - use foreach loops to set all animations at once
    [SerializeField] Animator[] vineAnims;

    // Settings
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] float _projectileSpeed;
    [SerializeField] float _secBetweenShots = 1.0f;
    [SerializeField] float InaccuracyDegrees = 5f;
    [SerializeField] float StunnedTimeSec = 5f;
    float IEnemyBehavior.stunDuration { get => StunnedTimeSec; set => StunnedTimeSec = value; }

    // Memebers
    GameObject _target;
    float _secondsTilShoot;
    public Transform head, barrel;

    // Get all animations
    void Start()
    {
        _secondsTilShoot = _secBetweenShots;
    }

    void FixedUpdate()
    {
        if (_secondsTilShoot > 0)
            _secondsTilShoot -= Time.deltaTime;
        if (_target != null)
        {
            head.LookAt(_target.transform);
            if (_secondsTilShoot <= 0)
            {
                Shoot(_target);
                _secondsTilShoot = _secBetweenShots;
            }
                
        }
    }

    /// <summary>
    /// Copied from TurretController
    /// </summary>
    /// <param name="target"></param>
    void Shoot(GameObject target)
    {
        GameObject clone = Instantiate(ProjectilePrefab, barrel.position, head.rotation);
        clone.GetComponent<Rigidbody>().AddForce(head.forward * _projectileSpeed, ForceMode.Impulse);
        Destroy(clone, 10);
    }

    /// <summary>
    /// Called once when the polyp should begin attacking.
    /// </summary>
    /// <param name="target"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    void IEnemyBehavior.Attack(GameObject target)
    {
        _target = target;
        _secondsTilShoot = _secBetweenShots;
        foreach (Animator anim in vineAnims)
        {
            anim.SetBool("Stun", false);
            anim.SetBool("Aggro", true);
        }
        print("Attack animation");
    }

    void IEnemyBehavior.Die()
    {
        _target = null;
        foreach (Animator anim in vineAnims)
        {
            anim.SetBool("Dying", true);
        }
        print("Death animation");
    }

    void IEnemyBehavior.Idle()
    {
        _target = null;
        foreach (Animator anim in vineAnims)
        {
            anim.SetBool("Stun", false);
            anim.SetBool("Aggro", false);
        }
        print("Idle animation");
    }

    void IEnemyBehavior.Stun()
    {
        _target = null;
        foreach (Animator anim in vineAnims)
        {
            anim.SetBool("Stun", true);
        }
        print("Stun animation.");
    }
}
