using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFlag : MonoBehaviour
{
    [SerializeField] FlagType pickup;
    public void TryPickup()
    {
        switch (pickup)
        {
            case FlagType.PowerCell:
                if (!GameManager.Instance.hasPowerCell)
                {
                    GameManager.Instance.CollectPowerCell();
                    Destroy(gameObject);
                }
                break;
            case FlagType.Scrubber:
                if (!GameManager.Instance.hasScrubber)
                {
                    GameManager.Instance.CollectScrubber();
                    Destroy(gameObject);
                }
                break;
            case FlagType.WeedEx:
                if (!GameManager.Instance.hasWeedEx)
                {
                    GameManager.Instance.CollectWeedEx();
                    Destroy(gameObject);
                }
                break;
        }
    }
}
