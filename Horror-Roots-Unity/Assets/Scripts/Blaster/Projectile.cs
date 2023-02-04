using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] float bulletVelocity;
    [SerializeField] float bulletTimer = 10f;


    public void Init(Vector3 destination)
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > bulletTimer)
            DestroyProjectile();
        transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * bulletVelocity;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        DestroyProjectile();
    }
}
