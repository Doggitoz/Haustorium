using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerVineController : MonoBehaviour
{
    [SerializeField] GameObject VineOne;
    [SerializeField] GameObject VineTwo;
    [SerializeField] Animator vineOneAnim;
    [SerializeField] Animator vineTwoAnim;

    public void Start()
    {

    }

    public void SetTrigger(string trigger, int num = 0)
    {
        if (num == 0)
        {
            vineOneAnim.SetTrigger(trigger);
        }
        else if (num == 1)
        {
            vineTwoAnim.SetTrigger(trigger);
        }
    }

}
