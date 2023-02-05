using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    [SerializeField] LayerMask layermask;
    Transform _Player;
    float dist;
    public float howClose;
    public Transform head, barrel;
    public GameObject _projectile;
    public int _projectileSpeed;
    public float fireRate;
    float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        dist = UnityEngine.Vector3.Distance(_Player.position, transform.position);
        if(dist <= howClose)
        {
            RaycastHit hit;
            UnityEngine.Vector3 dirToPlayer = _Player.position - _projectile.transform.position;
            if (Physics.Raycast(_projectile.transform.position, dirToPlayer, Mathf.Infinity, layermask))
            {
                head.LookAt(_Player);
                if (Time.time >= nextFire)
                {
                    nextFire = Time.time + 1f / fireRate;
                    shoot();
                }
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
