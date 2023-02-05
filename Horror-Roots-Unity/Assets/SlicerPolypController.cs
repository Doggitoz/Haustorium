using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerPolypController : MonoBehaviour
{
    [SerializeField] SlicerVineController vines;
    [SerializeField] GameObject vinesGO;
    [SerializeField] GameObject tip;
    [SerializeField] float damageAmt;
    float vineIdleTimer = 0f;
    float stunTimer = 0f;
    float damageTimer = 0f;
    bool aggro = false;
    bool stunned = false;
    [SerializeField] GameObject player;
    [SerializeField] PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (aggro)
        {
            AggroState();
        }
        else
        {
            IdleState();
        }
    }

    public void AggroState()
    {
        //THIS IS REALLY DUMB FOR LATER
        playerController.SetPlayerState(PlayerState.Immobile);
        tip.transform.position = tip.transform.position + (transform.position - player.transform.position).normalized * Time.deltaTime;
        playerController.PullTo(tip.transform.position, 20f);

    }

    public void IdleState()
    {
        if (vineIdleTimer > .3f)
        {
            int random = Random.Range(0, 12);

            if (random <= 1)
            {
                vines.SetTrigger("Wriggle", 1);
            }
            if (random >= 10)
            {
                vines.SetTrigger("Wriggle", 2);
            }
            if (random >= 4 || random <= 8)
            {
                vinesGO.transform.Rotate(new Vector3(0, 1, 0));
            }
            if (random == 3)
            {
                vinesGO.transform.Rotate(new Vector3(0, -1, 0));
            }
            vineIdleTimer = 0f;
        }
    }

    public void SetPlayerLocation(GameObject go)
    {
        player = go;
        tip.transform.position = go.transform.position;
    }

    public void ToggleAggro(bool val)
    {
        aggro = val;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            //THIS IS REALLY DUMB
            playerController.SetPlayerState(PlayerState.Default);
            Debug.Log("Shot");
            if (stunTimer > 1f)
            {
                stunned = true;
                vines.SetTrigger("Stun", 1);
                vines.SetTrigger("Stun", 2);
                stunTimer = 0f;
            }
        }
    }
}
