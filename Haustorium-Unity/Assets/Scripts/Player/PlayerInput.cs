using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] Blaster blaster;

    Vector2 movement = Vector2.zero;

    private void Update()
    {
        if (movement != Vector2.zero)
        {
            controller.Move(movement);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        controller.Look(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
            controller.Shoot();
    }

    public void OnFlashlight(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        controller.ToggleFlashlight();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!GameManager.Instance.canPause) return;
        
        if (GameManager.Instance.isPaused)
        {
            SaveData.Instance.SaveToJson();
            GameManager.Instance.SetPause(false);
        }
        else
        {
            GameManager.Instance.SetPause(true);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        controller.Interact();
    }

}
