using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseFlag : MonoBehaviour
{
    [SerializeField] FlagType type;
    [SerializeField] PodController pod;
    [SerializeField] MeshRenderer mesh;

    public void TryUsePickup()
    {   
        Debug.Log("try place");
        switch (type)
        {
            case FlagType.PowerCell:
                Debug.Log("try place cell");
                if (GameManager.GM.UsePowerCell())
                {
                    pod.FixPower();
                    EnableMesh();
                }
                break;
            case FlagType.Scrubber:
                Debug.Log("try place scrubber");
                if (GameManager.GM.UseScrubber())
                {
                    pod.ReplaceScrubber();
                    EnableMesh();
                }
                break;
            case FlagType.WeedEx:
                Debug.Log("try place weedex");
                if (GameManager.GM.UseWeedEx())
                {
                    pod.ClearObstruction();
                    EnableMesh();
                }
                break;

        }
    }

    void EnableMesh()
    {
        if (mesh == null) return;
        mesh.enabled = true;
    }
}
