using System.Collections;
using System.Collections.Generic;
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
    float yPitch = 0f;

    [Header("Blaster")]
    public Blaster blaster;
    [SerializeField] GameObject projectilePrefab;
    public LayerMask ProjectileLayerMask;
    [SerializeField] bool _canShoot = true;

    [Header("Flashlight")]
    public Flashlight flashlight;

    //Components
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
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

    public void Look(Vector2 values)
    {
        transform.Rotate(Vector3.up * values.x * Time.deltaTime * sensitivity);
        yPitch = Mathf.Clamp(yPitch + sensitivity * Time.deltaTime * values.y, -maxPitch, maxPitch);
        playerCam.transform.localEulerAngles = new Vector3(-yPitch, playerCam.transform.localEulerAngles.y, 0);
    }

    public void Move(Vector2 values)
    {
        Vector3 movement = new Vector3(values.x, 0, values.y) * Time.deltaTime * moveSpeed;
        //Switch statement to determine the calculation performed on the players movement speed
        switch (playerState)
        {
            case PlayerState.Immobile:
                movement *= 0;
                break;
            case PlayerState.Slowed:
                movement *= slownessMultiplier;
                break;
            default:
                break;

        }
        transform.Translate(movement);
    }

    public void ToggleFlashlight()
    {
        flashlight.ToggleFlashlight();
    }

    public void PullTo(Vector3 destination, float force)
    {
        rb.AddForce((destination - transform.position).normalized * force, ForceMode.Force);
    }

    #region Blaster
    public void Shoot()
    {
        if (!_canShoot)
        {
            return;
        }
        //Would like to add a simple timer here
        if (blaster.Shoot())
        {
            //Spawn projectile

            GameObject go = Instantiate(projectilePrefab);
            Projectile proj = go.GetComponent<Projectile>();
            go.transform.position = blaster.projectileLocation.transform.position;
            Vector3 temp = transform.rotation.eulerAngles;
            temp.x = playerCam.transform.rotation.eulerAngles.x;
            go.transform.rotation = Quaternion.Euler(temp);

        }
    }

    public void SetCanShoot(bool value) {
        _canShoot = value;
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