using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //public float damageTaken = .10f;
    public float DPS;
    bool damaging = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damaging)
        {
            float damageDealt = DPS * Time.deltaTime;
            PlayerManager.Instance.Controller.DealDamage(damageDealt);
            print("Damaged player for " + damageDealt);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.CompareTag("Player"))
       {
            damaging = true;
            //damage = Mathf.Min(damage + damageTaken * Time.deltaTime, 100.0f);
            //PlayerManager.Instance.Controller.DealDamage(damageTaken);
            //Destroy(gameObject);
       }
    }

    private void OnCollisionExit(Collision collision)
    {
        damaging = false;
    }
}
