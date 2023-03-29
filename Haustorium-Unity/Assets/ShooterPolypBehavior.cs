using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ShooterPolypBehavior : MonoBehaviour, IEnemyBehavior
{
    // Objects - use foreach loops to set all animations at once
    [SerializeField] Animator[] vineAnims;

    // Settings
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] float SecBetweenShots = 1.0f;
    [SerializeField] float InaccuracyDegrees = 5f;
    [SerializeField] float StunnedTimeSec = 5f;
    float IEnemyBehavior.stunDuration { get => StunnedTimeSec; set => StunnedTimeSec = value; }

    // Memebers
    GameObject _target;
    float _secondsTilShoot;

    // Get all animations
    void Start()
    {
        _secondsTilShoot = SecBetweenShots;
    }

    void FixedUpdate()
    {
        if (_secondsTilShoot > 0)
            _secondsTilShoot -= Time.deltaTime;
        if (_target != null && _secondsTilShoot <= 0)
            Shoot(_target);
    }

    /// <summary>
    /// Spawn a spread of projectiles moving toward target in a cone determined by InaccuracyDegrees
    /// </summary>
    /// <param name="target"></param>
    void Shoot(GameObject target)
    {
        // TODO
    }

    /// <summary>
    /// Called once when the polyp should begin attacking.
    /// </summary>
    /// <param name="target"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    void IEnemyBehavior.Attack(GameObject target)
    {
        _target = target;
        _secondsTilShoot = SecBetweenShots;
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
