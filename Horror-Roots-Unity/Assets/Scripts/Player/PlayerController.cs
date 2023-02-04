using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera playerCam;
    public Blaster blaster;
    public float sensitivity = 5f;
    public float moveSpeed = 5f;
    [Range(50, 85)] public float maxPitch;

    float yPitch = 0f;

    [SerializeField] GameObject projectilePrefab;
    public LayerMask ProjectileLayerMask;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
    }

    public void Look(Vector2 values)
    {
        transform.Rotate(Vector3.up * values.x * Time.deltaTime * sensitivity);
        yPitch = Mathf.Clamp(yPitch + sensitivity * Time.deltaTime * values.y, -maxPitch, maxPitch);
        playerCam.transform.localEulerAngles = new Vector3(-yPitch, playerCam.transform.localEulerAngles.y, 0);
    }

    public void Move(Vector2 values)
    {
        Vector3 movement = new Vector3(values.x, 0, values.y) * Time.deltaTime * moveSpeed;
        transform.Translate(movement);
    }

    public void Shoot()
    {
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
}
