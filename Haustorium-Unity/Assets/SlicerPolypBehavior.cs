using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SlicerPolypBehavior : MonoBehaviour, IEnemyBehavior
{
    [SerializeField] float StunnedTimeSec = 5f;
    [SerializeField] GameObject VineOne;
    [SerializeField] GameObject VineTwo;
    Animator vineOneAnim;
    Animator vineTwoAnim;

    GameObject vineTarget;

    float IEnemyBehavior.stunDuration { get => StunnedTimeSec; set => StunnedTimeSec = value; }

    void Start()
    {
        vineOneAnim = VineOne.GetComponent<Animator>();
        vineTwoAnim = VineTwo.GetComponent<Animator>();
        vineOneAnim.ResetTrigger("Wriggle");
        vineTwoAnim.ResetTrigger("Wriggle");
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
        vineOneAnim.Play("Wriggle");
        vineTwoAnim.Play("Wriggle");
        print("Attack behavior");
    }

    /// <summary>
    /// Called once by AI when polyp is killed by pesticide.
    /// </summary>
    void IEnemyBehavior.Die()
    {
        vineOneAnim.Play("VineDeath");
        vineTwoAnim.Play("VineDeath");
        print("Death animation");
        vineTarget = null;
    }

    /// <summary>
    /// Called once by AI when the polyp returns to idle.
    /// </summary>
    void IEnemyBehavior.Idle()
    {
        vineOneAnim.Play("VineIdle");
        vineTwoAnim.Play("VineIdle");
        print("Idle animation");
        vineTarget = null;
    }

    /// <summary>
    /// Called once by AI when the polyp is stunned by the player's blaster.
    /// </summary>
    void IEnemyBehavior.Stun()
    {
        vineOneAnim.Play("VineStun");
        vineTwoAnim.Play("VineStun");
        print("Stun animation");
        vineTarget = null;
    }
}
