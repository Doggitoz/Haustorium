using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] float bulletVelocity;
    [SerializeField] float bulletTimer = 10f;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > bulletTimer)
            DestroyProjectile();
    }

    public void Init()
    {
        rb.AddForce(transform.TransformDirection(Vector3.forward) * bulletVelocity, ForceMode.Impulse);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyProjectile();
    }
}
