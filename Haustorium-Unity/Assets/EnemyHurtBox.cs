using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyHurtBox : MonoBehaviour
{
    SphereCollider _sphereCollider;

    public delegate void OnShot();
    public event OnShot onShot;

    // Start is called before the first frame update
    void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.enabled = true;
        _sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            print("I've been shot by the player's gun");
            onShot.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
