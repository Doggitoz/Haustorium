using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("State")]
    [SerializeField] PlayerState playerState;

    [Header("Movement")]
    public float sensitivity = 5f;
    public float moveSpeed = 5f;
    public float slownessMultiplier = .5f;

    [Header("Camera")]
    public Camera playerCam;
    [Range(50, 85)] public float maxPitch;
    public float fov = 60f;
    float yPitch = 0f;

    [Header("Audio")]
    [SerializeField] AudioSource footstepsSource;
    [SerializeField] AudioSource playerEffects;
    [SerializeField] AudioClip[] footsteps;

    [Header("Blaster")]
    public Blaster blaster;
    [SerializeField] AudioClip ShootSfx;
    
    [SerializeField] bool _canShoot = true;

    [Header("Flashlight")]
    public Flashlight flashlight;

    [Header("Health")]
    [SerializeField] PlayerHealth health;

    [Header("Inventory")]
    public float reach;
    public LayerMask InteractLayerMask;
    [SerializeField] InventorySystem inv;

    [Header("Misc")]
    public float timeBetweenFootsteps = .7f;
    Vector3 spawnLocation;
    float footstepsTimer = 0f;

    //Components
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        spawnLocation = transform.position;
        playerCam.fieldOfView = fov;
    }

    void FixedUpdate()
    {
        switch (playerState)
        {
            case PlayerState.Immobile:
                break;
            case PlayerState.Slowed:
                footstepsTimer += Time.deltaTime * slownessMultiplier;
                break;
            default:
                footstepsTimer += Time.deltaTime;
                break;

        }
        if (transform.position.y < -100f)
        {
            transform.position = spawnLocation;
        }
    }

    #region Player State

    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    public void SetPlayerState(PlayerState ps)
    {
        playerState = ps;
    }

    #endregion

    #region Input
    public void Look(Vector2 values)
    {
        if (GameManager.Instance.isPaused) return;
        transform.Rotate(Vector3.up * values.x * Time.deltaTime * sensitivity);
        yPitch = Mathf.Clamp(yPitch + sensitivity * Time.deltaTime * values.y, -maxPitch, maxPitch);
        playerCam.transform.localEulerAngles = new Vector3(-yPitch, playerCam.transform.localEulerAngles.y, 0);
    }

    public void Move(Vector2 values)
    {
        if (GameManager.Instance.isPaused) return;
        Vector3 movement = new Vector3(values.x, 0, values.y) * Time.deltaTime * moveSpeed;
        //Switch statement to determine the calculation performed on the players movement speed
        switch (playerState)
        {
            case PlayerState.Immobile:
                return;
            case PlayerState.Slowed:
                movement *= slownessMultiplier;
                break;
            default:
                break;
        }

        //I would eventually like to replace this with an animation event called when the foot lands on the ground
        if (footstepsTimer > timeBetweenFootsteps)
        {
            footstepsTimer = 0f;
            PlayFootstep();
        }
        transform.Translate(movement);
    }

    public void ToggleFlashlight()
    {
        if (GameManager.Instance.isPaused) return;
        flashlight.ToggleFlashlight();
    }

    public void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hit, reach, InteractLayerMask))
        {
            GameObject go = hit.collider.gameObject;
            Debug.Log("trying to interact with: " + go.name);
            if (go.CompareTag("Item"))
            {
                Debug.Log("Interacting with item");
                inv.Add(go.GetComponent<ItemObject>().referenceItem);
                Destroy(go);
            }
            else if (go.CompareTag("Trigger"))
            {
                Debug.Log("Interacting with trigger");
                go.GetComponent<TriggerObject>().Trigger();
            }
        }
    }

    public void PlayFootstep()
    {
        int random = Random.Range(0, footsteps.Length);
        footstepsSource.PlayOneShot(footsteps[random]);
    }

    #region Blaster
    public void Shoot()
    {
        if (!_canShoot) return;
        if (GameManager.Instance.isPaused) return;

        if (blaster.Shoot(transform, playerCam.transform))
        {
            playerEffects.PlayOneShot(ShootSfx);
        }
    }

    public void SetCanShoot(bool value) {
        _canShoot = value;
    }
    #endregion

    #endregion

    public void PullTo(Vector3 destination, float force)
    {
        rb.AddForce((destination - transform.position).normalized * force, ForceMode.Force);
    }

    

    #region Health
    
    public void DealDamage(float damage)
    {
        health.TakeDamage(damage);
        if (!health.IsAlive())
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        GameManager.Instance.SetState(GameState.Death);
    }

    #endregion
}

public enum PlayerState
{
    Default, Immobile, Slowed
    
    // Default = Full access to movement
    // Immobile = No movement
    // Slowed = Restricted movement speed
}