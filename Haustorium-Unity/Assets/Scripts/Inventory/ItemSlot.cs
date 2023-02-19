using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image m_icon;

    [SerializeField]
    private TextMeshProUGUI m_label;

    public void Set(InventoryItem item)
    {
        m_icon.sprite = item.data.icon;
        m_label.text = item.data.displayName;
        
    }
}
