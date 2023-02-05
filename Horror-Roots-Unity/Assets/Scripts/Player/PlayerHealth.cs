using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 100.00f;

    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        PostProcessing.PP.SetVignetteColor("8E3A30");
        PostProcessing.PP.SetVignetteIntensity(1 - (health / maxHealth));
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}