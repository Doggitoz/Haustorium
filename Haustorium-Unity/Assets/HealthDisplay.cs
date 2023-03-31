using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] PlayerHealth health;
    [SerializeField] TMP_Text text;

    private float m_health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_health != health.health)
        {
            m_health = health.health;
            text.text = "HP: " + Mathf.Round(m_health);
        }
    }
}
