using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    float dist;
    public float howClose;
    public Transform head, barrel;
    public GameObject _projectile;
    public int _projectileSpeed;
    public float fireRate;
    float nextFire;

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(PlayerManager.Instance.Transform.position, transform.position);
        if(dist <= howClose)
        {
            head.LookAt(PlayerManager.Instance.Transform);
            if(Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                shoot();
            }
            
        }
    }

    void shoot()
    {
        GameObject clone = Instantiate(_projectile, barrel.position, head.rotation);
        clone.GetComponent<Rigidbody>().AddForce(head.forward * _projectileSpeed);
        Destroy(clone, 10);
    }
}
