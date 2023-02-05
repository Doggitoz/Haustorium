using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPostProcessing : MonoBehaviour
{

    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            PostProcessing.PP.ResetVignette();
            timer = 0f;
            return;
        }
        PostProcessing.PP.SetVignetteIntensity(timer / 10f);
        PostProcessing.PP.SetVignetteColor("8E3A30");
    }
}
