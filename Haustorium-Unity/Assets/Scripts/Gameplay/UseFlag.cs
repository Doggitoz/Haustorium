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
        switch (type)
        {
            case FlagType.PowerCell:
                if (GameManager.Instance.UsePowerCell())
                {
                    pod.FixPower();
                    EnableMesh();
                }
                break;
            case FlagType.Scrubber:
                if (GameManager.Instance.UseScrubber())
                {
                    pod.ReplaceScrubber();
                    EnableMesh();
                }
                break;
            case FlagType.WeedEx:
                if (GameManager.Instance.UseWeedEx())
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
