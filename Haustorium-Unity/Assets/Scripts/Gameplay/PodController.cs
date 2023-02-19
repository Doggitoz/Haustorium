using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodController : MonoBehaviour
{
    [SerializeField] string TubeObstructionMessage;
    [SerializeField] string ReplaceScrubberMessage;
    [SerializeField] string PowerFailureMessage;

    //Bools for escaping
    bool clearedObstruction = false;
    bool replacedScrubber = false;
    bool fixedPower = false;

    public void TryEscape()
    {   
        if (!fixedPower)
        {
            //Have logic to display error message PowerFailureMessage
            Debug.Log(PowerFailureMessage);
            return;
        }
        if (!clearedObstruction)
        {
            //Have logic to display error message TubeObstructionMessage
            Debug.Log(TubeObstructionMessage);
            return;
        }
        if (!replacedScrubber)
        {
            //Have logic to display error message ReplaceScrubberMessage
            Debug.Log(ReplaceScrubberMessage);
            return;
        }
        
        Debug.Log("Escaped!");
        GameManager.Instance.SetState(GameState.Escaped);
    }

    public void ClearObstruction()
    {
        clearedObstruction = true;
    }

    public void ReplaceScrubber()
    {
        replacedScrubber = true;
    }

    public void FixPower()
    {
        fixedPower = true;
    }

}
