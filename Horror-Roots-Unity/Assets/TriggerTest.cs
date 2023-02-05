using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    Vector3 one;
    Vector3 two;
    bool up = false;
    private void Start()
    {
        one = transform.position;
        two = transform.position + Vector3.up;
    }
    public void ToggleState()
    {
        if (up)
        {
            transform.position = one;
            up = false;
        }
        else
        {
            transform.position = two;
            up = true;
        }
    }
}
