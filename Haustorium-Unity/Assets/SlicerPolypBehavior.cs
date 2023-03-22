using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SlicerPolypBehavior : MonoBehaviour, IEnemyBehavior
{
    [SerializeField] float StunnedTimeSec = 5f;
    [SerializeField] GameObject VineOne;
    [SerializeField] GameObject VineTwo;
    [SerializeField] Animator vineOneAnim;
    [SerializeField] Animator vineTwoAnim;

    float IEnemyBehavior.stunDuration { get => StunnedTimeSec; set => StunnedTimeSec = value; }

    void IEnemyBehavior.Attack(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    void IEnemyBehavior.Die()
    {
        throw new System.NotImplementedException();
    }

    void IEnemyBehavior.Idle()
    {
        vineOneAnim.SetTrigger("Wriggle");
        vineTwoAnim.SetTrigger("Wriggle");
    }

    void IEnemyBehavior.Stun()
    {
        vineOneAnim.SetTrigger("Stun");
        vineTwoAnim.SetTrigger("Stun");
    }

        // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
