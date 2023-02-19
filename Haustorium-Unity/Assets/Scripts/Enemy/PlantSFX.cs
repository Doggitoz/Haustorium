using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSFX : MonoBehaviour
{
    public AudioClip[] plantNoises;
    float timer = 0f;
    public AudioSource localSFX;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetState() != GameState.Playing) return;
        timer += Time.deltaTime;
        if (timer > 2.5f)
        {
            timer = 0f;
            int rand = Random.Range(0, 255);
            if (rand > 240)
            {
                int randIndex = Random.Range(0, plantNoises.Length);
                localSFX.PlayOneShot(plantNoises[randIndex]);
            }
        }
    }
}
