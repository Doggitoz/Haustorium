using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] int energy;
    [SerializeField] int cost;
    [SerializeField] int max;
    [Tooltip("Per second")] [SerializeField] float recharge;
    [SerializeField] float cooldown;

    [SerializeField] GameObject projectileLocation;

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

    //Returns T/F on whether the shot worked (not on cooldown)
    public bool Shoot(Transform player, Transform playerCam)
    {
        if (energy <= 0f)
        {
            return false;
        }
        if (shootTimer > cooldown)
        {
            rechargeTimer = 0f;
            energy -= cost;
            //Spawn bullet
            GameObject go = Instantiate(ProjectilePrefab);
            go.transform.position = projectileLocation.transform.position;
            Vector3 temp = player.rotation.eulerAngles;
            temp.x = playerCam.rotation.eulerAngles.x;
            go.transform.rotation = Quaternion.Euler(temp);
            Projectile proj = go.GetComponent<Projectile>();
            proj.Init();
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

}
