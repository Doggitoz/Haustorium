using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullTest : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.2f)
        {
            controller.PullTo(transform.position, 100f);
            timer = 0f;
        }
    }
}
