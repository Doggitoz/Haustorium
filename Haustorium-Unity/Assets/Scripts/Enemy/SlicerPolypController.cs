using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class SlicerPolypController : MonoBehaviour
{
    [SerializeField] SlicerVineController vines;
    [SerializeField] GameObject vinesGO;
    [SerializeField] GameObject tip;
    [SerializeField] float damageAmt;
    float vineIdleTimer = 0f;
    float stunTimer = 0f;
    float damageTimer = 0f;
    bool stunned = false;
    EnemyAI ai;

    // Start is called before the first frame update
    void Awake()
    {
        ai = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetState() != GameState.Playing) return;
        if (TryAggroPlayer())
        {
            damageTimer += Time.deltaTime;
            AggroState();
        }
        else
        {
            IdleState();
        }
    }

    public bool TryAggroPlayer()
    {
        return ai.CheckForPlayer();
    }

    public void AggroState()
    {
        //THIS IS REALLY DUMB FOR LATER
        //playerController.SetPlayerState(PlayerState.Immobile);
        tip.transform.position = tip.transform.position + (transform.position - PlayerManager.Instance.Player.transform.position).normalized * Time.deltaTime;
        PlayerManager.Instance.Controller.PullTo(tip.transform.position, 20f);
        if (damageTimer > .5f)
        {
            damageTimer = 0f;
            PlayerManager.Instance.Controller.DealDamage(5f);
        }

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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.gameObject.CompareTag("Projectile"))
        {
            //THIS IS REALLY DUMB
            PlayerManager.Instance.Controller.SetPlayerState(PlayerState.Default);
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
