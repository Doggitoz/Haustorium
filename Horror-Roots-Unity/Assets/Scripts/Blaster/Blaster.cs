using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] int energy;
    [SerializeField] int cost;
    [SerializeField] int max;
    [Tooltip("Per second")] [SerializeField] float recharge;
    [SerializeField] float cooldown;
    [SerializeField] bool canShoot;

    public GameObject projectileLocation;

    float rechargeTimer = 0f;
    float shootTimer = 0f;

    private void Start()
    {
        energy = max;
    }

    private void Update()
    {
        rechargeTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;
        if (rechargeTimer >= 1f / recharge)
        {
            if (energy < max)
            {
                rechargeTimer = 0f;
                energy++;
            }
        }
    }

    //Returns T/F on whether the shoot worked (not on cooldown)
    public bool Shoot()
    {
        if (!canShoot)
        {
            return false;
        }
        if (energy <= 0f)
        {
            return false;
        }
        if (shootTimer > cooldown)
        {
            rechargeTimer = 0f;
            energy -= cost;
            return true;
        }
        return false;
    }

    public int GetEnergy()
    {
        return energy;
    }

    public float GetEnergyPercentage()
    {
        return energy / max;
    }

    public void SetCanShoot(bool val)
    {
        canShoot = val;
    }

}
