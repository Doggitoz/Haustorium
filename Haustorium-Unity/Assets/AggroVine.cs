using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class AggroVine : MonoBehaviour
{
    [SerializeField] EnemyAI controllingAI;
    [SerializeField] bool invertWave;
    Quaternion defaultRotation;
    Vector3 defaultScale;
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
        defaultScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (aggro)
        {
            gameObject.transform.LookAt(controllingAI.Target.transform);
            if (invertWave)
            {
                gameObject.transform.Rotate(0, 0, 180);
            }
        }
           

        if (!aggro && controllingAI.State == EnemyAI.EnemyState.Aggro)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, 2 * gameObject.transform.localScale.z);
            aggro = true;
        }
        else if (aggro && controllingAI.State != EnemyAI.EnemyState.Aggro)
        {
            gameObject.transform.rotation = defaultRotation;
            gameObject.transform.localScale = defaultScale;
            aggro = false;
        }
    }
}
