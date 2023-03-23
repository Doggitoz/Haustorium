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
    }

    // Update is called once per frame
    void Update()
    {
        if (vineTarget != null)
        {
            // DRAG TARGET
        }
    }

    /// <summary>
    /// Point one or both vines at the target and apply a force to drag it in
    /// </summary>
    /// <param name="target"></param>
    void IEnemyBehavior.Attack(GameObject target)
    {
        vineTarget = target;
        print("Attack behavior");
    }

    void IEnemyBehavior.Die()
    {
        print("Death animation");
        vineTarget = null;
    }

    void IEnemyBehavior.Idle()
    {
        print("Idle animation");
        vineTarget = null;
    }

    void IEnemyBehavior.Stun()
    {
        print("Stun animation");
        vineTarget = null;
    }
}
