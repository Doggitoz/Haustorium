using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantrapBehavior : MonoBehaviour, IEnemyBehavior
{
    [SerializeField] float StunnedTimeSec = 5f;
    [SerializeField] GameObject Vine;
    Animator vineAnim;

    GameObject vineTarget;

    float IEnemyBehavior.stunDuration { get => StunnedTimeSec; set => StunnedTimeSec = value; }

    void Start()
    {
        vineAnim = Vine.GetComponent<Animator>();
    }

    // If the vine has a target, attempt to drag it in
    void FixedUpdate()
    {
        if (vineTarget != null)
        {
            // DRAG TARGET
        }
    }

    /// <summary>
    /// Called once by AI when the polyp should begin attacking.
    /// Point one or both vines at the target and apply a force to drag it in
    /// </summary>
    /// <param name="target"></param>
    void IEnemyBehavior.Attack(GameObject target)
    {
        vineTarget = target;
        vineAnim.SetBool("Aggro", true);
        vineAnim.SetBool("Stunned", false);
        print("Attack behavior");
    }

    /// <summary>
    /// Called once by AI when polyp is killed by pesticide.
    /// </summary>
    void IEnemyBehavior.Die()
    {
        print("Death animation");
        vineAnim.SetBool("Dying", true);
        vineTarget = null;
    }

    /// <summary>
    /// Called once by AI when the polyp returns to idle.
    /// </summary>
    void IEnemyBehavior.Idle()
    {
        print("Idle animation");
        vineTarget = null;
        vineAnim.SetBool("Aggro", false);
        vineAnim.SetBool("Stunned", false);
    }

    /// <summary>
    /// Called once by AI when the polyp is stunned by the player's blaster.
    /// </summary>
    void IEnemyBehavior.Stun()
    {
        print("Stun animation");
        vineTarget = null;
        vineAnim.SetBool("Stunned", true);
    }
}
