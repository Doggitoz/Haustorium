using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class AggroVine : MonoBehaviour
{
    [SerializeField] EnemyAI controllingAI;
    Quaternion defaultRotation;
    bool aggro = false;

    // Start is called before the first frame update
    void Start()
    {
        if (controllingAI == null)
        {
            print("vine missing reference to a controlling EnemyAI: " + gameObject);
            Destroy(this);
            return;
        }
        defaultRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (aggro)
            gameObject.transform.LookAt(controllingAI.Target.transform);

        if (!aggro && controllingAI.State == EnemyAI.EnemyState.Aggro)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 2);
            aggro = true;
        }
        else if(aggro && controllingAI.State != EnemyAI.EnemyState.Aggro)
        {
            gameObject.transform.rotation = defaultRotation;
            gameObject.transform.localScale = Vector3.one;
            aggro = false;
        }
    }
}
