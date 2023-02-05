using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light _flashlight;
    bool _forceOff = false;

    public void ToggleFlashlight()
    {
        if (_forceOff)
        {
            return;
        }

        _flashlight.enabled = !_flashlight.enabled;

    }

    public void TurnFlashlightOff()
    {
        _flashlight.enabled = false;
    }

    public void TurnFlashlightOn()
    {
        _flashlight.enabled = true;
    }

}
