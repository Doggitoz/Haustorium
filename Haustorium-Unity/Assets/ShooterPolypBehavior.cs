using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShooterPolypBehavior : MonoBehaviour, IEnemyBehavior
{
    // Objects - use foreach loops to set all animations at once
    [SerializeField] GameObject[] vines;
    Animator[] vineAnims;

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

        foreach (GameObject vine in vines)
        {
            Animator anim = vine.GetComponent<Animator>();
            if (anim != null)
            {
                vineAnims.Append(anim);
            }
        }
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
            // TODO
        }
        print("Attack animation");
    }

    void IEnemyBehavior.Die()
    {
        _target = null;
        foreach (Animator anim in vineAnims)
        {
            // TODO
        }
        print("Death animation");
    }

    void IEnemyBehavior.Idle()
    {
        _target = null;
        foreach (Animator anim in vineAnims)
        {
            // TODO
        }
        print("Idle animation");
    }

    void IEnemyBehavior.Stun()
    {
        _target = null;
        foreach (Animator anim in vineAnims)
        {
            // TODO
        }
        print("Stun animation.");
    }
}
