using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseFlag : MonoBehaviour
{
    [SerializeField] FlagType type;
    [SerializeField] PodController pod;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] GameObject GameObject;

    public void TryUsePickup()
    {   
        switch (type)
        {
            case FlagType.PowerCell:
                if (GameManager.Instance.UsePowerCell())
                {
                    pod.FixPower();
                    EnableMesh();
                    EnableObject();
                }
                break;
            case FlagType.Scrubber:
                if (GameManager.Instance.UseScrubber())
                {
                    pod.ReplaceScrubber();
                    EnableMesh();
                    EnableObject();
                }
                break;
            case FlagType.WeedEx:
                if (GameManager.Instance.UseWeedEx())
                {
                    pod.ClearObstruction();
                    EnableMesh();
                    EnableObject();
                }
                break;

        }
    }

    void EnableMesh()
    {
        if (mesh == null) return;
        mesh.enabled = true;
    }

    void EnableObject()
    {
        if (GameObject == null) return;
        GameObject.SetActive(true);
    }
}
