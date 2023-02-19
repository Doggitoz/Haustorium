using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerObject : MonoBehaviour
{
    public bool oneTimeUse = false;
    bool hasBeenUsed = false;

    [SerializeField] TriggerEvent triggerEvent;
    [System.Serializable] public class TriggerEvent : UnityEvent { }

    public void Trigger()
    {
        if (oneTimeUse)
        {
            if (!hasBeenUsed)
            {
                hasBeenUsed = true;
                triggerEvent.Invoke();
            }
        }
        else
        {
            triggerEvent.Invoke();
        }
    }

}

[System.Serializable]
public enum FlagType
{
    PowerCell, Scrubber, WeedEx
}
