using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Item;
using UnityEngine.EventSystems;
using System;
//Slot Class
public class Slot : MonoBehaviour
{
    public Item.ItemData slotItem
    {
        get { return slotItem; }
        set
        {
            if (slotitem != null)
            {
                Debug.Log(slotitem.name);
                throw new Exception();
            }
        }
    }
    private Item.ItemData slotitem;
    public int slotID;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public bool sellableSlot = false;
    public bool mouseOver = false;
    public GameObject tooltip;
    public bool toolbarSlot;
    [SerializeField] private GameObject highlight;

    public void SetItem(Inventory.Slot slot)
    {
        if (slot.item != null)
        {
            slotItem = slot.item;
            itemIcon.sprite = Resources.Load<Sprite>("Sprites/Items/" + slot.item.sprite);
            //itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.count.ToString();
        }
    }
    public void SetEmpty()
    {
        slotItem = null;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 1);
        quantityText.text = "";
    }

    public void SetHighlight(bool isOn)
    {
        highlight.SetActive(isOn);
    }

    public void ActivateTooltip()
    {
        if (slotItem != null)
        {
            tooltip.gameObject.SetActive(true);
            tooltip.GetComponent<Tooltip>().slot = this;
            tooltip.GetComponent<Tooltip>().SetTooltip();
        }

    }

    public void DeActivateTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }
}
