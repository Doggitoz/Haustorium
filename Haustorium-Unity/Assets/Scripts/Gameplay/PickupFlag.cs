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
                if (!GameManager.GM.hasPowerCell)
                {
                    GameManager.GM.CollectPowerCell();
                    Destroy(gameObject);
                }
                break;
            case FlagType.Scrubber:
                if (!GameManager.GM.hasScrubber)
                {
                    GameManager.GM.CollectScrubber();
                    Destroy(gameObject);
                }
                break;
            case FlagType.WeedEx:
                if (!GameManager.GM.hasWeedEx)
                {
                    GameManager.GM.CollectWeedEx();
                    Destroy(gameObject);
                }
                break;
        }
    }
}
