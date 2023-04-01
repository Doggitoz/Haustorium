using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoReadout : MonoBehaviour
{
    [SerializeField] TMP_Text m_TextMeshPro;
    [SerializeField] Blaster m_Blaster;
    float ammo;

    // Update is called once per frame
    void Update()
    {
        if (m_Blaster.GetEnergy() != ammo)
        {
            ammo = m_Blaster.GetEnergy();
            m_TextMeshPro.text = ammo.ToString();
        }
    }
}
