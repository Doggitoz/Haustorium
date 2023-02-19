using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageTaken = .10f;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Player")
       {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            damage = Mathf.Min(damage + damageTaken * Time.deltaTime, 100.0f);
            controller.DealDamage(damageTaken);
            Destroy(gameObject);
       }
    }
}
