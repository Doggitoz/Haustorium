using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessing : MonoBehaviour
{

    //PostProcessing stuff
    [SerializeField] Volume volume;
    private Bloom b;
    private Vignette vg;

    //Default settings
    float _defaultVignetteIntensity;
    Color _defaultVignetteColor;

    #region PostProcessing Singleton
    static private PostProcessing pp;
    static public PostProcessing PP { get { return pp; } }

    void CheckManagerInScene()
    {

        if (pp == null)
        {
            pp = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void Awake()
    {
        CheckManagerInScene();
        volume.profile.TryGet(out b);
        volume.profile.TryGet(out vg);
    }

    private void Start()
    {
        _defaultVignetteIntensity = vg.intensity.value;
        _defaultVignetteColor = ((Color)vg.color);
    }

    public void SetVignetteIntensity(float val)
    {
        vg.intensity.value = Mathf.Clamp(val, 0f, 1f);
    }

    public void SetVignetteColor(Color color)
    {
        vg.color.Override(color);
    }

    public void SetVignetteColor(string hex)
    {
        hex = hex.Replace("#", ""); //in case the string is formatted #FFFFFF 

        byte a = 255;
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        vg.color.Override(new Color32(r, g, b, a));
    }

    public void ResetVignette()
    {
        vg.intensity.value = _defaultVignetteIntensity;
    }
    
}
