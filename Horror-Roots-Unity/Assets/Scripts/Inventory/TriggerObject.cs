using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerObject : MonoBehaviour
{
    public bool oneTimeUse = false;
    [SerializeField] TriggerEvent triggerEvent;
    bool hasBeenUsed = false;
    [System.Serializable]
    public class TriggerEvent : UnityEvent { }

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
